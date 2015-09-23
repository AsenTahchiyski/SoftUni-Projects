package contracts;

import java.util.Queue;

/**
 * Defines methods for accessing and manipulating an achievement engine.
 */
public interface AchievementManager {
    
    /**
     * Updates the achievement manager.
     */
    void update();
    
    
    /**
     * Gets all the achievements of the manager.
     * @return
     */
    Queue<Achievement> getNewlyEarnedAchievements();
}
