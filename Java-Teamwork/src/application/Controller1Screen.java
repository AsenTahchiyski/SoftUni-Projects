package application;

import contracts.ControlledScreen;
import javafx.event.ActionEvent;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.MenuItem;
import javafx.scene.layout.AnchorPane;

import java.net.URL;
import java.util.ResourceBundle;


/**
 * Created by borislavivanov on 9/22/15.
 */
public class Controller1Screen implements Initializable, ControlledScreen {

    public Button startButton;
    public MenuItem showUnits;
    public Button exitButton;
    public AnchorPane background;
    public ControllerScreens controller1Screen;
    public MenuItem upgrades;

    public void startAction(ActionEvent actionEvent) {
        startButton.setOnAction(event -> controller1Screen.setScreen(Main.menu2ID));
    }

    public void startActionShowUnits(ActionEvent actionEvent) {
    }

    public void exitAction(ActionEvent actionEvent) {
        exitButton.setOnAction(event -> AlertBox.display());
    }


    @Override
    public void setScreenParent(ControllerScreens screenPage) {
        controller1Screen = screenPage;
    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {

    }

    public void buyUp(ActionEvent actionEvent) {
        upgrades.setOnAction(event -> controller1Screen.setScreen(Main.menu3ID));
    }
}
