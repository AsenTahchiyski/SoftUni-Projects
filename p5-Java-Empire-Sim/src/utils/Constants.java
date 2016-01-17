package utils;


import ui.BattlesSimulator;
import javafx.scene.image.Image;

public class Constants {
    public static final String WINDOW_TITLE = "Battle mode";
    public static final int BATTLE_WINDOW_WIDTH = 800;
    public static final int BATTLE_WINDOW_HEIGHT = 600;

    public static final Image BACKGROUND_IMAGE = new Image(BattlesSimulator.class.getResource("images/background.jpg").toString());

    public static final Image PLAYER_PLANET_IMAGE = new Image(BattlesSimulator.class.getResource("images/planet3.png").toString());
    public static final double PLAYER_PLANET_HEIGHT = 70;
    public static final double PLAYER_PLANET_WIDTH = 90;
    public static final int PLAYER_PLANET_X = 55;
    public static final int PLAYER_PLANET_Y = 180;

    public static final Image ENEMY_VARMALUS_PLANET_IMAGE = new Image(BattlesSimulator.class.getResource("images/planet1.jpg").toString());
    public static final double ENEMY_VARMALUS_PLANET_HEIGHT = 90;
    public static final double ENEMY_VARMALUS_PLANET_WIDTH = 90;
    public static final double ENEMY_VARMALUS_PLANET_X = 350;
    public static final double ENEMY_VARMALUS_PLANET_Y = 250;

    public static final Image ENEMY_SLEKON_PLANET_IMAGE = new Image(BattlesSimulator.class.getResource("images/planet2.png").toString());
    public static final double ENEMY_SLEKON_PLANET_HEIGHT = 110;
    public static final double ENEMY_SLEKON_PLANET_WIDTH = 110;
    public static final double ENEMY_SLEKON_PLANET_X = 420;
    public static final double ENEMY_SLEKON_PLANET_Y = 25;

    public static final Image ENEMY_ZAKROS_PLANET_IMAGE = new Image(BattlesSimulator.class.getResource("images/planet4.png").toString());
    public static final double ENEMY_ZAKROS_PLANET_HEIGHT = 70;
    public static final double ENEMY_ZAKROS_PLANET_WIDTH = 120;
    public static final double ENEMY_ZAKROS_PLANET_X = 570;
    public static final double ENEMY_ZAKROS_PLANET_Y = 155;

    public static final Image ENEMY_NURUTA_PLANET_IMAGE = new Image(BattlesSimulator.class.getResource("images/planet5.png").toString());
    public static final double ENEMY_NURUTA_PLANET_HEIGHT = 120;
    public static final double ENEMY_NURUTA_PLANET_WIDTH = 120;
    public static final double ENEMY_NURUTA_PLANET_X = 400;
    public static final double ENEMY_NURUTA_PLANET_Y = 445;

    public static final Image MISSILE_IMAGE = new Image(BattlesSimulator.class.getResource("images/missile.png").toString());
    public static final double MISSILE_HEIGHT = 30;
    public static final double MISSILE_WIDTH = 70;
    public static final double MISSILE_X = 125;
    public static final double MISSILE_Y = 190;
}
