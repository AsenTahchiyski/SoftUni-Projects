package models;

import contracts.Achievement;
import contracts.Unit;

public class EmpireAchievement implements Achievement {

    private String name;
    private Unit requiredUnit;
    private double requiredPrecision;
    private int requiredExponent;
    private boolean isEarned;

    /**
     * Initializes a new EmpireAchievement.
     * @param name	the name of the achievement
     * @param requiredUnit	the required unit
     * @param requiredPrecision	the required precision
     * @param requiredExponent	the required exponent
     */
    public EmpireAchievement(String name, Unit requiredUnit, double requiredPrecision, int requiredExponent) {
	this.setName(name);
	this.setRequiredUnit(requiredUnit);
	this.setRequiredPrecision(requiredPrecision);
	this.setRequiredExponent(requiredExponent);
    }

    @Override
    public String getName() {
	return this.name;
    }

    private void setName(String name) {
	this.name = name;
    }

    @Override
    public Unit getRequiredUnit() {
	return this.requiredUnit;
    }

    private void setRequiredUnit(Unit requiredUnit) {
	this.requiredUnit = requiredUnit;
    }

    @Override
    public double getRequiredPrecision() {
	return this.requiredPrecision;
    }

    private void setRequiredPrecision(double requiredPrecision) {
	this.requiredPrecision = requiredPrecision;
    }

    @Override
    public int getRequiredExponent() {
	return this.requiredExponent;
    }

    private void setRequiredExponent(int requiredExponent) {
	this.requiredExponent = requiredExponent;
    }

    @Override
    public boolean isEarned() {
	return this.isEarned;
    }

    private void setEarned(boolean isEarned) {
	this.isEarned = isEarned;
    }

    public void earnIfQualifying() {
	if (!this.isEarned()) {
	    int currentExponent = this.getRequiredUnit().getExponent();
	    if (this.requiredExponent < currentExponent || this.requiredExponent == currentExponent
		    && this.requiredPrecision <= this.getRequiredUnit().getPrecision()) {
		this.setEarned(true);
	    }
	}
    }
}
