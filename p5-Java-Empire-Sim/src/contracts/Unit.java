package contracts;

import models.HugeInteger;

/**
 * Defines methods for accessing and manipulating a Unit.
 */
public interface Unit {
    
	/**
	 * Gets the precision of the unit. 
	 * Combine with exponent to get the total quantity of the unit.
	 * @return	the precision of the unit
	 */
	double getPrecision();
	
	void setPrecision(double precision);
	
	/**
	 * Gets the exponent of the unit. 
	 * Combine with precision to get the total quantity of the unit.
	 * The exponent base is always 10.
	 * @return	the exponent of the unit
	 */
	int getExponent();

	void setExponent(int exponent);
	
	/**
	 * Gets the resource cost of buying 1 unit.
	 * @return	the resource cost
	 */
	HugeInteger getResourceCost();

	void setResourceCost(HugeInteger cost);
	
	/**
	 * Gets the unit cost of buying 1 unit. 
	 * Unit costs are the number of output units needed to produce 1 unit of this kind.
	 * @return	the unit cost
	 * @see #getOutputUnits()
	 */
	HugeInteger getUnitCost();

	void setUnitCost(HugeInteger count);
	
	/**
	 * Gets the output production per second of this unit.
	 * Output production is of output units.
	 * @return	the output production
	 * @see #getOutputUnits()
	 */
	HugeInteger getOutputProduction();

	void setOutputProduction(HugeInteger count);
	
	/**
	 * Gets the name of the unit.
	 * @return	the name
	 */
	String getName();
	
	/**
	 * Gets the flavor text of the unit.
	 * @return	the flavor text
	 */
	String getFlavorText();

	/**
	 * Gets the output units of this unit.
	 * @return	an array of the output units
	 */
	Unit[] getOutputUnits();

	/**
	 * Gets the level of the current unit.
	 * @return	an integer indicating unit's level.
	 */
	@Deprecated
	int getLevel();
	
	int getUpgradeLevel(UpgradeTypes upgradeType);
	
	void setUpgradeLevel(UpgradeTypes upgradeType, int level);
	
	int getSpawnCount();

	HugeInteger calculateUpgradeCost(UpgradeTypes upgradeType, int level);
	
}
