package contracts;

import java.util.List;

/**
 * Defines methods for accessing a UnitTree.
 */
public interface UnitTree {
    
	/**
	 * Gets the root of the tree.
	 * @return	the root of the tree
	 * @see Unit#getOutputUnits()
	 */
	Unit getRootUnit();

	Iterable<Unit> getAllUnits();

	boolean contains(Unit unit);

	Unit getChild(Unit unit);

	void addDescendantsRecursively(Unit current, List<Unit> list);
}
