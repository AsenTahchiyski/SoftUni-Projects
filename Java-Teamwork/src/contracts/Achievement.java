package contracts;

/**
 * Defines methods for accessing and modifying achievements.
 */
public interface Achievement {

    String getName();

    Unit getRequiredUnit();

    /**
     * Gets the precision of the unit. Combine with exponent to get the total
     * quantity of the unit.
     * 
     * @return the precision of the unit
     */
    double getRequiredPrecision();

    /**
     * Gets the exponent of the unit. Combine with precision to get the total
     * quantity of the unit. The exponent base is always 10.
     * 
     * @return the exponent of the unit
     */
    int getRequiredExponent();
    
    boolean isEarned();
}
