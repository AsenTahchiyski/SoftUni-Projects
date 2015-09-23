package models.guiModels;

import javafx.scene.image.Image;

public class Planet extends GameObject {
    public Planet(Image image, double width, double height, double x, double y) {
        super(image, width, height, x, y);
    }
    public Planet(double x,double y){
        super(null,0,0,x,y);
    }
}
