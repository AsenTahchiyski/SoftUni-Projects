package controllers;

import contracts.Engine;
import contracts.Unit;
import contracts.UnitTree;
import contracts.UpgradeTypes;
import models.EmpireUnit;
import models.EmpireUnitTree;
import models.HugeInteger;
import java.util.ArrayList;

/**
 * Base engine class, containint the basic functionality, als keeping the player data and unit trees.
 */
public class EmpireEngine implements Engine {

    private UnitTree bacon;
    private UnitTree freedom;
    private UnitTree democracy;
    private long moolah;
    private long elapsedSeconds;
	private boolean isRunning = false;

    public EmpireEngine() {
		this.bacon = new EmpireUnitTree();
		this.freedom = new EmpireUnitTree();
		this.democracy = new EmpireUnitTree();
		this.moolah = 0;
		this.elapsedSeconds = 0;
    }

    /**
     * Update method parses through all unit trees and updates all unit data, increases descendants with output
     * quantities.
     */
    @Override
    public void update() {

		long current = System.currentTimeMillis();
		long deltaT = current - this.elapsedSeconds * 1000;
		long deltaTSeconds = deltaT / 1000;
		if (deltaTSeconds > 0) {
			Unit currentBaconNode = this.bacon.getRootUnit();
			while(this.bacon.getChild(currentBaconNode) != null) {
				Unit child = this.bacon.getChild(currentBaconNode);
				HugeInteger increase = currentBaconNode.getOutputProduction();
				child.setPrecision(child.getPrecision() + increase.getPrecision());
				child.setExponent(child.getExponent() + increase.getExponent());
				currentBaconNode = child;
			}

			Unit currentFreedomNode = this.freedom.getRootUnit();
			while(this.freedom.getChild(currentFreedomNode) != null) {
				HugeInteger increase = currentFreedomNode.getOutputProduction();
				this.getResourceAmount(this.freedom).setPrecision(increase.getPrecision());
				this.getResourceAmount(this.freedom).setExponent(increase.getExponent());
				currentFreedomNode = this.freedom.getChild(currentFreedomNode);
			}

			HugeInteger currentDemocracy = this.getResourceAmount(this.democracy);
			Unit senator = this.democracy.getRootUnit(); // where is the Senator in the tree?
			HugeInteger democracyIncrease = new HugeInteger(
					senator.getOutputProduction().getPrecision() * deltaTSeconds % 10,
					(int) (senator.getOutputProduction().getExponent() * deltaTSeconds / (deltaTSeconds % 10)));
			currentDemocracy.setPrecision(currentDemocracy.getPrecision() + democracyIncrease.getPrecision());
			currentDemocracy.setExponent(currentDemocracy.getExponent() + democracyIncrease.getExponent());
		}
    }

    /**
     * Retrieves specific unit tree.
     * @param resourceName    the resource tree to get
     * @return EmpireUnitTree containing all units in the given tree.
     */
    @Override
    public UnitTree getUnits(String resourceName) {
	switch (resourceName) {
		case "bacon":
			return this.bacon;
		case "freedom":
			return this.freedom;
		case "democracy":
			return this.democracy;
		default:
			throw new IllegalArgumentException("No such resource.");
		}
    }

    /**
     * Checks whether a specific upgrade for a given unit can be performed.
     * @param unit            the unit to upgrade
     * @param upgradeType    the upgrade type
     * @return Boolean value stating is the upgrade currently allowed or not.
     */
    @Override
    public boolean peekUpgrade(Unit unit, UpgradeTypes upgradeType) {
		if (unit == null) {
			throw new IllegalArgumentException("Unit to check upgrades for should not be null.");
		}

		int upgradeLevel = unit.getUpgradeLevel(upgradeType);
		HugeInteger upgradeCost = unit.calculateUpgradeCost(upgradeType, upgradeLevel + 1);
		if (unit.getExponent() >= upgradeCost.getExponent() && unit.getPrecision() > upgradeCost.getPrecision()) {
			return true;
		}

		return false;
    }

    /**
     * Applies an upgrade for a given unit, if all requirements are met.
     * Uses the peekUpgrade to check whether the conditions have been met.
     * @param unit            the unit to upgrade
     * @param upgradeType    the upgrade type
     * @return Boolean value stating whether the operation has been successful.
     */
    @Override
    public boolean upgrade(Unit unit, UpgradeTypes upgradeType) {
		boolean canUpgrade = this.peekUpgrade(unit, upgradeType);
		if (canUpgrade) {
			HugeInteger upgradeCost = unit.calculateUpgradeCost(upgradeType, unit.getUpgradeLevel(upgradeType));
			// value to be edited later
			unit.setOutputProduction(new HugeInteger(unit.getOutputProduction().getPrecision() * 2, unit.getExponent()));
			if (this.bacon.contains(unit)) {
				Unit child = this.bacon.getChild(unit);
				child.setExponent(child.getExponent() - upgradeCost.getExponent()); // subtract child unit cost
				child.setPrecision(child.getPrecision() - upgradeCost.getPrecision());
				unit.setUpgradeLevel(upgradeType, unit.getUpgradeLevel(upgradeType) + 1);
				return true;
			} else if (this.freedom.contains(unit)) {
				Unit child = this.freedom.getChild(unit);
				child.setExponent(child.getExponent() - upgradeCost.getExponent());
				child.setPrecision(child.getPrecision() - upgradeCost.getPrecision());
				unit.setUpgradeLevel(upgradeType, unit.getUpgradeLevel(upgradeType) + 1);
				return true;
			} else if (this.democracy.contains(unit)) {
				Unit child = this.democracy.getChild(unit);
				child.setExponent(child.getExponent() - upgradeCost.getExponent());
				child.setPrecision(child.getPrecision() - upgradeCost.getPrecision());
				unit.setUpgradeLevel(upgradeType, unit.getUpgradeLevel(upgradeType) + 1);
				return true;
			}
		}

		return false;
    }

    /**
     * Checks whether a given amount of specific unit can be purchased.
     * @param unit    the unit to check
     * @param count    the amount to check
     * @return Boolean value stating whether the operation can be performed or not.
     */
    @Override
    public boolean peekBuyUnits(Unit unit, HugeInteger count) {
		HugeInteger totalUnitCost = new HugeInteger(unit.getUnitCost().getPrecision() * count.getPrecision(),
			unit.getExponent() * count.getExponent());
		HugeInteger totalResourceCost = new HugeInteger(unit.getResourceCost().getPrecision() * count.getPrecision(),
			unit.getExponent() * count.getExponent());

		if (this.bacon.contains(unit)) {
			Unit child = this.bacon.getChild(unit);
			HugeInteger totalChildAmount = new HugeInteger(child.getPrecision(), child.getExponent());
			if (totalChildAmount.compareTo(totalUnitCost) >= 0 &&
					this.getResourceAmount(this.bacon).compareTo(totalResourceCost) >= 0) {
				return true;
			}
		} else if (this.freedom.contains(unit)) {
			Unit child = this.freedom.getChild(unit);
			HugeInteger totalChildAmount = new HugeInteger(child.getPrecision(), child.getExponent());
			if (totalChildAmount.compareTo(totalUnitCost) >= 0 &&
					this.getResourceAmount(this.freedom).compareTo(totalResourceCost) >= 0) {
				return true;
			}
		} else if (this.democracy.contains(unit)) {
			Unit child = this.democracy.getChild(unit);
			HugeInteger totalChildAmount = new HugeInteger(child.getPrecision(), child.getExponent());
			if (totalChildAmount.compareTo(totalUnitCost) >= 0 &&
					this.getResourceAmount(this.democracy).compareTo(totalResourceCost) >= 0) {
				return true;
			}
		}

		return false;
    }

    /**
     * Performs the actual units purchase, after checking whether conditions have been met with peekBuyUnits.
     * @param unit    the unit type to buy
     * @param count    the amount of units to purchase
     * @return Boolean value indicating whether the operation has been successful.
     */
    @Override
    public boolean buyUnits(Unit unit, HugeInteger count) {
		if (this.peekBuyUnits(unit, count)) {
			HugeInteger totalCost = new HugeInteger(unit.getPrecision() * count.getPrecision(),
					unit.getExponent() * count.getExponent());

			if (this.bacon.contains(unit)) {
				HugeInteger newResourceAmount = new HugeInteger(
						this.getResourceAmount(this.bacon).getPrecision() - totalCost.getPrecision(),
						this.getResourceAmount(this.bacon).getExponent() - count.getExponent());
				this.setResourceAmount(this.bacon, newResourceAmount);

				HugeInteger newChildAmount = new HugeInteger(
						this.getResourceAmount(this.bacon).getPrecision() - unit.getUnitCost().getPrecision(),
						this.getResourceAmount(this.bacon).getExponent() - unit.getUnitCost().getExponent());
				this.bacon.getChild(unit).setPrecision(newChildAmount.getPrecision());
				this.bacon.getChild(unit).setExponent(newChildAmount.getExponent());
			} else if (this.freedom.contains(unit)) {
				HugeInteger newResourceAmount = new HugeInteger(
						this.getResourceAmount(this.freedom).getPrecision() - totalCost.getPrecision(),
						this.getResourceAmount(this.freedom).getExponent() - count.getExponent());
				this.setResourceAmount(this.freedom, newResourceAmount);

				HugeInteger newChildAmount = new HugeInteger(
						this.getResourceAmount(this.freedom).getPrecision() - unit.getUnitCost().getPrecision(),
						this.getResourceAmount(this.freedom).getExponent() - unit.getUnitCost().getExponent());
				this.freedom.getChild(unit).setPrecision(newChildAmount.getPrecision());
				this.freedom.getChild(unit).setExponent(newChildAmount.getExponent());
			} else if (this.democracy.contains(unit)) {
				HugeInteger newResourceAmount = new HugeInteger(
						this.getResourceAmount(this.democracy).getPrecision() - totalCost.getPrecision(),
						this.getResourceAmount(this.democracy).getExponent() - count.getExponent());
				this.setResourceAmount(this.democracy, newResourceAmount);

				HugeInteger newChildAmount = new HugeInteger(
						this.getResourceAmount(this.democracy).getPrecision() - unit.getUnitCost().getPrecision(),
						this.getResourceAmount(this.democracy).getExponent() - unit.getUnitCost().getExponent());
				this.democracy.getChild(unit).setPrecision(newChildAmount.getPrecision());
				this.democracy.getChild(unit).setExponent(newChildAmount.getExponent());
			}

			unit.setPrecision(unit.getPrecision() + count.getPrecision());
			unit.setExponent(unit.getExponent() + count.getExponent());
			return true;
		}

		return false;
    }

    /**
     * Leaps time forward in the engine, related to some upgrade operations.
     * @param seconds    the amount of seconds to leap
     */
    @Override
    public void leapSeconds(long seconds) {
		this.elapsedSeconds -= seconds;
		this.update();
    }

    /**
     * Provides the current player score based on the resources available.
     * @return Player score in type of Long.
     */
    @Override
    public long getScore() {
		long score = 0;
		score += Math.sqrt(this.moolah); // to be balanced once game is working
		score += Math.sqrt(this.getResourceAmount(this.bacon).getExponent());
		score += Math.sqrt(this.getResourceAmount(this.democracy).getExponent());
		score += Math.sqrt(this.getResourceAmount(this.freedom).getExponent());
		return score;
    }

    /**
     * Initializes the engine state.
     */
    public void initialize() {
        Unit bacon = new EmpireUnit(40, 1, "Bacon", "bacon flavor text",
                new HugeInteger(0, 0), new HugeInteger(0, 0), new HugeInteger(0, 0)) {
            @Override
            public int getUpgradeLevel(UpgradeTypes upgradeType) {
                return 0;
            }

            @Override
            public void setUpgradeLevel(UpgradeTypes upgradeType, int level) {

            }

            @Override
            public int getSpawnCount() {
                return 0;
            }
        };
        this.bacon.addDescendantsRecursively(bacon, new ArrayList<>());

        Unit soldier = new EmpireUnit(10, 1, "Soldier", "soldier flavor text",
                new HugeInteger(10, 0), new HugeInteger(0, 0), new HugeInteger(0, 0)) {
            @Override
            public int getUpgradeLevel(UpgradeTypes upgradeType) {
                return 0;
            }

            @Override
            public void setUpgradeLevel(UpgradeTypes upgradeType, int level) {

            }

            @Override
            public int getSpawnCount() {
                return 0;
            }
        };
        this.democracy.addDescendantsRecursively(soldier, new ArrayList<>());

        this.moolah += 10;
    }

    /**
     * Provides the current Bacon resource amount.
     * @return HugeInteger Bacon
     */
    @Override
	public HugeInteger getBaconAmount() {
		return this.getResourceAmount(this.bacon);
	}

    /**
     * Provides the current Democracy resource amount.
     * @return HugeInteger Democracy
     */
    @Override
	public HugeInteger getDemocracyAmount() {
		return this.getResourceAmount(this.democracy);
	}

    /**
     * Provides the current Freedom resource amount.
     * @return HugeInteger Freedom
     */
    @Override
	public HugeInteger getFreedomAmount() {
		return this.getResourceAmount(this.democracy);
	}

    /**
     * Provides the current Moolah (money) resource amount.
     * @return Long Moolah (money)
     */
    @Override
	public long getMoolahAmount() {
		return this.moolah;
	}

	/**
	 * Sets the current Bacon resource amount.
     * @param amount New resource amount to set.
     */
	@Override
	public void setBaconAmount(HugeInteger amount) { this.setResourceAmount(this.bacon, amount); }

	/**
	 * Sets the current Democracy resource amount.
     * @param amount New resource amount to set.
	 */
	@Override
	public void setDemocracyAmount(HugeInteger amount) {
		this.setResourceAmount(this.democracy, amount);
	}

	/**
	 * Sets the current Freedom resource amount.
     * @param amount New resource amount to set.
     */
	@Override
	public void setFreedomAmount(HugeInteger amount) {
		this.setResourceAmount(this.democracy, amount);
	}

	/**
	 * Sets the current Moolah (money) resource amount.
     * @param amount New resource amount to set.
	 */
	@Override
	public void setMoolahAmount(long amount) {
        if (amount < 0) {
            throw new IllegalArgumentException("Moolah amount cannot be negative.");
        }

        this.moolah = amount; }

	/**
	 * Initializes the engine and starts constantly updating its state.
	 */
	@Override
	public void run() {
		this.initialize();
		this.isRunning = true;
		while(this.isRunning) {
			this.update();
		}
	}

	/**
	 * Stops the engine from running.
	 */
	@Override
	public void stop() {
		this.isRunning = false;
	}

    /**
     * Returns the current resource for a given unit tree, where resource is at the bottom of the tree.
     * @param tree The resource tree.
     * @return HugeInteger Resource amount
     */
	private HugeInteger getResourceAmount(UnitTree tree) {
		HugeInteger resourceAmount = new HugeInteger(0, 0);
		Unit child = tree.getRootUnit();
		do {
			if (tree.getChild(child) != null) {
				child = tree.getChild(child);
			} else {
				resourceAmount.setExponent(child.getExponent());
				resourceAmount.setPrecision(child.getPrecision());
				return resourceAmount;
			}
		} while(true);
	}

    /**
     * Sets the resource amount for a given unit/resource tree.
     * @param tree Resource tree to update amount in.
     * @param amount Amount to set, HugeInteger.
     */
	private void setResourceAmount(UnitTree tree, HugeInteger amount) {
		Unit child = tree.getRootUnit();
		do {
			if (tree.getChild(child) != null) {
				child = tree.getChild(child);
			} else {
				child.setPrecision(amount.getPrecision());
				child.setExponent(amount.getExponent());
			}
		} while(true);
	}
}
