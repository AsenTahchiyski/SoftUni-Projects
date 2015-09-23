package ui;

import contracts.Engine;
import javafx.animation.Animation;
import javafx.animation.TranslateTransitionBuilder;
import javafx.beans.binding.Bindings;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.effect.DropShadow;
import javafx.scene.image.ImageView;
import javafx.scene.layout.VBox;
import javafx.scene.layout.VBoxBuilder;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.Text;
import javafx.stage.Modality;
import javafx.stage.Stage;
import javafx.util.Duration;
import models.HugeInteger;
import models.guiModels.Missile;
import models.guiModels.Planet;
import utils.Constants;
import utils.EnhancedRandom;

import java.util.*;


public class BattlesSimulator {

    private Animation missileAnimation;
    private Group missiles;

    /**
     * List planets in the galactic.
     */
    private List<Planet> planets;

    /**
     * Missile starting point. It starting point is the location of the player planet.
     */
    private int missilePointX = Constants.PLAYER_PLANET_X-120;
    private int missilePointY = Constants.PLAYER_PLANET_Y-160;

    Text endGameText;
    private VBox endGameBox;

    private final int deviationX = 62;
    private final int deviationY = 45;

    /**
     * When the missile reach the chosen enemy planet the animation ends.
     * The missile wont be rendered again on the screen.
     */
    private Boolean missileAnimationEnded = false;

    /**
     * Removes the difference in the coordinates of the planets.
     */
    private int enemyPlanetDeviation = 150;

    private double unstableRotationRatio = 0;

    private Engine engine;

    public BattlesSimulator(Engine engine){
        this.engine = engine;
    }

    /**
     * Displays the battles window.
     * This method render all the objects in it.
     */
    public void display(){
        Stage window = new Stage();
        window.initModality(Modality.APPLICATION_MODAL);
        window.setTitle(Constants.WINDOW_TITLE);
        window.setWidth(Constants.BATTLE_WINDOW_WIDTH);
        window.setHeight(Constants.BATTLE_WINDOW_HEIGHT);

        final ImageView background = new ImageView(Constants.BACKGROUND_IMAGE);


        this.planets = new ArrayList<>(Arrays.asList(
            new Planet(
                    Constants.PLAYER_PLANET_IMAGE,
                    Constants.PLAYER_PLANET_WIDTH,
                    Constants.PLAYER_PLANET_HEIGHT,
                    Constants.PLAYER_PLANET_X,
                    Constants.PLAYER_PLANET_Y),
            new Planet(
                    Constants.ENEMY_NURUTA_PLANET_IMAGE,
                    Constants.ENEMY_NURUTA_PLANET_WIDTH,
                    Constants.ENEMY_NURUTA_PLANET_HEIGHT,
                    Constants.ENEMY_NURUTA_PLANET_X,
                    Constants.ENEMY_NURUTA_PLANET_Y),
            new Planet(
                    Constants.ENEMY_VARMALUS_PLANET_IMAGE,
                    Constants.ENEMY_VARMALUS_PLANET_WIDTH,
                    Constants.ENEMY_VARMALUS_PLANET_HEIGHT,
                    Constants.ENEMY_VARMALUS_PLANET_X,
                    Constants.ENEMY_VARMALUS_PLANET_Y),

            new Planet(
                    Constants.ENEMY_SLEKON_PLANET_IMAGE,
                    Constants.ENEMY_SLEKON_PLANET_WIDTH,
                    Constants.ENEMY_SLEKON_PLANET_HEIGHT,
                    Constants.ENEMY_SLEKON_PLANET_X,
                    Constants.ENEMY_SLEKON_PLANET_Y),
            new Planet(
                    Constants.ENEMY_ZAKROS_PLANET_IMAGE,
                    Constants.ENEMY_ZAKROS_PLANET_WIDTH,
                    Constants.ENEMY_ZAKROS_PLANET_HEIGHT,
                    Constants.ENEMY_ZAKROS_PLANET_X,
                    Constants.ENEMY_ZAKROS_PLANET_Y)));

        Missile missile = new Missile(
                Constants.MISSILE_IMAGE,
                Constants.MISSILE_WIDTH,
                Constants.MISSILE_HEIGHT,
                Constants.MISSILE_X,
                Constants.MISSILE_Y);

        this.missiles = new Group(missile.getImageView());
        this.missiles.setEffect(new DropShadow(2, Color.color(1, 0, 0)));

        final Group foreground = new Group();

        for (Planet planet : this.planets) {
            foreground.getChildren().add(planet.getImageView());
        }

        foreground.setEffect(new DropShadow());

        this.endGameText = new Text();
        this.endGameText.setFont(new Font(50));
        this.endGameText.setStyle("-fx-stroke: red;");


        this.endGameBox = VBoxBuilder.create()
                .children(this.endGameText)
                .translateX(270)
                .translateY(170)
                .build();
        this.endGameText.setVisible(false);

        final Group root = new Group(background, foreground, this.missiles, this.endGameBox);
        this.missiles.setVisible(false);
        Scene scene = new Scene(root, Constants.BATTLE_WINDOW_WIDTH, Constants.BATTLE_WINDOW_HEIGHT);

        window.setScene(scene);
        scene.setOnMousePressed(e -> this.startAnimation(e.getX(), e.getY()));
        battleProcess();
        window.showAndWait();
    }

    /**
     * It animates the missile and moves it from the starting point to the enemy planet location.
     * @param x coordinate of the enemy planet location.
     * @param y coordinate of the enemy planet location.
     */
    private void startAnimation(double x, double y){
        Planet chosenPlanet = findPlanetByXY(x, y);

        if (!missileAnimationEnded){
            this.missiles.setVisible(true);
        }

        if (chosenPlanet == null){
            this.setMissileMovement(x, y);
            this.missiles.setRotate(unstableRotationRatio+=2);

            if (this.missilePointX == x-this.enemyPlanetDeviation && this.missilePointY == y-this.enemyPlanetDeviation){
                this.missilePointX = Constants.PLAYER_PLANET_X-120;
                this.missilePointY = Constants.PLAYER_PLANET_Y-160;
                this.missiles.setVisible(false);
                this.missileAnimation.stop();
                this.missileAnimationEnded = true;
            }
        }
        else{
            this.setMissileRotation(chosenPlanet.getY());
            this.setMissileMovement(chosenPlanet.getX(), chosenPlanet.getY());

            if (this.missilePointX == chosenPlanet.getX()-this.enemyPlanetDeviation && this.missilePointY == chosenPlanet.getY()-enemyPlanetDeviation){
                this.missilePointX = Constants.PLAYER_PLANET_X-120;
                this.missilePointY = Constants.PLAYER_PLANET_Y-160;
                this.missiles.setVisible(false);
                this.missileAnimation.stop();
                this.missileAnimationEnded = true;
            }
        }

        this.missileAnimation = TranslateTransitionBuilder.create()
                .node(this.missiles)
                .fromX(this.missilePointX)
                .toX(this.missilePointX)
                .fromY(this.missilePointY)
                .toY(this.missilePointY)
                .duration(Duration.millis(0.1))
                .onFinished(e -> this.startAnimation(x, y))
                .build();

        this.missileAnimation.play();
    }

    /**
     * Moves the missile closer to the enemy planet.
     * @param x coordinate of the enemy planet location.
     * @param y coordinate of the enemy planet location.
     */
    private void setMissileMovement(double x, double y) {
        if(this.missilePointX < x - this.enemyPlanetDeviation){
            this.missilePointX++;
        }
        else if (this.missilePointX > x - this.enemyPlanetDeviation){
            this.missilePointX--;
        }

        if (this.missilePointY < y - this.enemyPlanetDeviation){
            this.missilePointY++;
        }
        else if (this.missilePointY > y - this.enemyPlanetDeviation){
            this.missilePointY--;
        }
    }

    /**
     * rotates the missile towards an enemy planet
     * it rotates the image by degrees
     * @param planetY y coordinate of the enemy planet.
     */
    private void setMissileRotation(double planetY) {
        if (this.missilePointY < planetY){
            double ratio = planetY - this.missilePointY;
            double degrees = (ratio / 10) - 15;
            this.missiles.setRotate(degrees);
        }
        else if (this.missilePointY > planetY){
            double ratio = this.missilePointY - planetY;
            double degrees = (ratio / 10) + 260;
            this.missiles.setRotate(degrees);
        }
    }

    /**
     * Finds a planet by x and y coordinates.
     * This method checks if the x and y coordinates belong to any of the enemies planets.
     * If true, return the planet and start the animation, else, stop animation.
     * @param x coordinate of wanted planet.
     * @param y coordinate of wanted planet.
     * @return Planet by given x and y coordinates.
     */
    private Planet findPlanetByXY(double x, double y) {

        Optional<Planet> matchedPlanet = this.planets
                .stream()
                .filter(planet -> Math.pow(
                        (x - (planet.getX() + this.deviationX)), 2)
                        + Math.pow((y - (planet.getY() + this.deviationY)), 2)
                        <= Math.pow((planet.getWidth() / 2)+5, 2)
                        && planet != this.planets.get(0))
                .findFirst();

        try {
            return matchedPlanet.get();
        }
        catch (NoSuchElementException e){
            return null;
        }
    }

    /**
     * Process the battle
     * Generates all planet resources by pseudo random number.
     * The exponent is a random number with range [0.5 current resource; 1.25 of current resource]
     * The precision is a random number with range [0.5 current resource; 1.5 of current resource]
     */
    private void battleProcess(){
        int enemyBaconExponent = EnhancedRandom.nextInt(
                (this.engine.getBaconAmount().getExponent() / 2),
                this.engine.getBaconAmount().getExponent() + (this.engine.getBaconAmount().getExponent() / 2));
        double enemyBaconPrecision = EnhancedRandom.nextDouble(
                (this.engine.getBaconAmount().getPrecision() / 2),
                this.engine.getBaconAmount().getPrecision() + (this.engine.getBaconAmount().getPrecision() / (2 + 2)));

        int enemyFreedomExponent = EnhancedRandom.nextInt(
                (this.engine.getFreedomAmount().getExponent() / 2),
                this.engine.getFreedomAmount().getExponent() + (this.engine.getFreedomAmount().getExponent() / 2));
        double enemyFreedomPrecision = EnhancedRandom.nextDouble(
                (this.engine.getFreedomAmount().getPrecision() / 2),
                this.engine.getFreedomAmount().getPrecision() + (this.engine.getFreedomAmount().getPrecision() / (2 + 2)));

        int enemyDemocracyExponent = EnhancedRandom.nextInt(
                (this.engine.getDemocracyAmount().getExponent() / 2),
                this.engine.getDemocracyAmount().getExponent() + (this.engine.getDemocracyAmount().getExponent() / 2));
        double enemyDemocracyPrecision = EnhancedRandom.nextDouble(
                (this.engine.getDemocracyAmount().getPrecision() / 2),
                this.engine.getDemocracyAmount().getPrecision() + (this.engine.getDemocracyAmount().getPrecision() / (2 + 2)));

        long enemyMoolah = EnhancedRandom.nextLong((this.engine.getMoolahAmount() / 2), this.engine.getMoolahAmount() + (this.engine.getMoolahAmount() / 2));
        HugeInteger enemyBaconAmounth = new HugeInteger(enemyBaconPrecision, enemyBaconExponent);
        HugeInteger enemyFreedomAmounth = new HugeInteger(enemyFreedomPrecision, enemyFreedomExponent);
        HugeInteger enemyDemocracyAmounth = new HugeInteger(enemyDemocracyPrecision, enemyDemocracyExponent);

        Boolean baconContention = this.engine.getBaconAmount().compareTo(enemyBaconAmounth) == 1;
        Boolean freedomContention = this.engine.getFreedomAmount().compareTo(enemyFreedomAmounth) == 1;
        Boolean democracyContention = this.engine.getDemocracyAmount().compareTo(enemyDemocracyAmounth) == 1;
        Boolean moolahContention = this.engine.getMoolahAmount() > enemyMoolah;

        addBonusAmountIfPlayerWins(baconContention, freedomContention, democracyContention, moolahContention);

        popUpGameEndText(baconContention, freedomContention, democracyContention, moolahContention);
    }

    /**
     * If one of the booleans is true, you win, else lose.
     * This method makes a VBox on the screen visible and adds some text on it.
     * @param baconContention is a boolean which is true if the player resource is more than the resource of the enemy planet, else false.
     * @param freedomContention is a boolean which is true if the player resource is more than the resource of the enemy planet, else false.
     * @param democracyContention is a boolean which is true if the player resource is more than the resource of the enemy planet, else false.
     * @param moolahContention is a boolean which is true if the player resource is more than the resource of the enemy planet, else false.
     */
    private void popUpGameEndText(Boolean baconContention, Boolean freedomContention, Boolean democracyContention, Boolean moolahContention) {
        if (baconContention || freedomContention || democracyContention || moolahContention){
            this.endGameText.textProperty().bind(Bindings.concat("YOU WIN"));
            this.endGameText.setVisible(true);
        }
        else{
            this.endGameText.textProperty().bind(Bindings.concat("YOU LOSE"));
            this.endGameText.setVisible(true);
        }
    }

    /**
     * Every boolean which is true, adds bonus points to the current resource.
     * @param baconContention is a boolean which is true if the player resource is more than the resource of the enemy planet, else false.
     * @param freedomContention is a boolean which is true if the player resource is more than the resource of the enemy planet, else false.
     * @param democracyContention is a boolean which is true if the player resource is more than the resource of the enemy planet, else false.
     * @param moolahContention is a boolean which is true if the player resource is more than the resource of the enemy planet, else false.
     */
    private void addBonusAmountIfPlayerWins(Boolean baconContention, Boolean freedomContention, Boolean democracyContention, Boolean moolahContention) {
        if (baconContention){
            this.engine.setBaconAmount(calcNewResourceAmount(this.engine.getBaconAmount()));
        }
        if (freedomContention){
            this.engine.setFreedomAmount(calcNewResourceAmount(this.engine.getFreedomAmount()));
        }
        if (democracyContention){
            this.engine.setDemocracyAmount(calcNewResourceAmount(this.engine.getDemocracyAmount()));
        }
        if (moolahContention){
            this.engine.setMoolahAmount(this.engine.getMoolahAmount() + (this.engine.getMoolahAmount() / 20));
        }
    }

    /**
     * This method increases the exponent by five.
     * @param resourceAmount current resource amount which is needed to calculate the new resource amount.
     * @return the new resource amount of points, after adding the bonus.
     */
    private HugeInteger calcNewResourceAmount (HugeInteger resourceAmount) {
        int upgradeExponent = 5;
        double currentPrecision = resourceAmount.getPrecision();
        int currentExponent = resourceAmount.getExponent();
        HugeInteger newAmount = new HugeInteger(currentPrecision,currentExponent + upgradeExponent);
        return newAmount;
    }
}
