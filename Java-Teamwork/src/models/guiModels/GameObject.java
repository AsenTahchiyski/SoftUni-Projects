package models.guiModels;

import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import utils.Constants;
import utils.Messages;
import utils.exceptions.GameObjectOutOfWindowBoundsException;


/**
 * These objects are significant for the graphic user interface of the game.
 * The Image argument represents the image of the object rendered on the screen.
 * This class is abstract and is inherited by the main graphic objects in this program.
 *
 * @return object with specified image, width, height, x and y axis.
 * @ see Image
 */
public abstract class GameObject {
    private ImageView imageView;
    private double x;
    private double y;
    private double width;
    private double height;

    /**
     * Creates an object that can be printed on the screen.
     *
     * @param image represents the image of the object rendered in specific width and height.
     * @param width determine the width of the image on the GUI window.
     * @param height determine the height of the image on the GUI window.
     * @param x determine the x axis of the image on the GUI window.
     * @param y determine the y axis of the image on the GUI window.
     */
    protected GameObject (Image image, double width, double height, double x, double y){
        this.setWidth(width);
        this.setHeight(height);
        try {
            this.setX(x);
        } catch (GameObjectOutOfWindowBoundsException e) {
            e.printStackTrace();
        }
        try {
            this.setY(y);
        } catch (GameObjectOutOfWindowBoundsException e) {
            e.printStackTrace();
        }
        imageViewLoader(image, width, height, x, y);
    }

    public ImageView getImageView(){
        return this.imageView;
    }

    public double getX(){
        return this.x;
    }

    /**
     * Set the x coordinate point of the game object.
     * @throws GameObjectOutOfWindowBoundsException if the given coordinate is out of the window bounds.
     */
    private void setX(double x)throws GameObjectOutOfWindowBoundsException{
        if (x > Constants.BATTLE_WINDOW_WIDTH || x < 0){
            throw new GameObjectOutOfWindowBoundsException(
                    String.format(Messages.gameObjectOutOfWindowBoundsMessage, 'x', Constants.BATTLE_WINDOW_WIDTH));
        }
        this.x = x;
    }

    public double getY(){
        return this.y;
    }

    /**
     * Set the y coordinate point of the game object.
     * @throws GameObjectOutOfWindowBoundsException if the given coordinate is out of the window bounds.
     */
    private void setY(double y) throws GameObjectOutOfWindowBoundsException {
        if (y > Constants.BATTLE_WINDOW_HEIGHT || y < 0){
            throw new GameObjectOutOfWindowBoundsException(
                    String.format(Messages.gameObjectOutOfWindowBoundsMessage, 'y', Constants.BATTLE_WINDOW_HEIGHT));
        }
        this.y = y;
    }

    public double getWidth(){
        return this.width;
    }

    private void setWidth(double width){
        this.width = width;
    }

    public double getHeight(){
        return this.height;
    }

    private void setHeight(double height){
        this.height = height;
    }

    /**
     * Loads the ImageView with specified parameters.
     * @param width determine the width of the image on the GUI window.
     * @param height determine the height of the image on the GUI window.
     * @param x determine the x axis of the image on the GUI window.
     * @param y determine the y axis of the image on the GUI window.
     */
    private void imageViewLoader(Image image, double width, double height, double x, double y) {
        final ImageView imageView = new ImageView(image);
        setWidthAndHeight(imageView, width, height);
        setVector(imageView, x, y);
        this.imageView = imageView;
    }

    private void setVector(ImageView image, double x, double y) {
        image.setTranslateX(x);
        image.setTranslateY(y);
    }

    private void setWidthAndHeight(ImageView image, double width, double height) {
        image.setFitHeight(height);
        image.setFitWidth(width);
    }
}