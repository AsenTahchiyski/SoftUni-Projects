package models;

public class HugeInteger {
    private double precision;
    private int exponent;

    public HugeInteger(double precision, int exponent) {
		this.setPrecision(precision);
		this.setExponent(exponent);
    }

    public double getPrecision() {
	return precision;
    }

    public void setPrecision(double precision) {
	this.precision = precision;
    }

    public int getExponent() {
	return exponent;
    }

    public void setExponent(int exponent) {
		if (exponent < 0) {
			throw new IllegalArgumentException("Exponent cannot be negative.");
		}

	this.exponent = exponent;
    }

    public int compareTo(HugeInteger other) {
	if (this.getExponent() == other.getExponent()) {
	    if (this.getPrecision() == other.getPrecision()) {
		return 0;
	    }

	    if (this.getPrecision() > other.getPrecision()) {
		return 1;
	    }
	}

	if (this.getExponent() > other.getExponent())
	    return 1;
	return -1;
    }
}
