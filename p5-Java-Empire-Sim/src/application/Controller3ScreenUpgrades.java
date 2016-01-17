package application;

import contracts.ControlledScreen;
import javafx.fxml.Initializable;

import java.net.URL;
import java.util.ResourceBundle;

/**
 * Created by borislavivanov on 9/23/15.
 */
public class Controller3ScreenUpgrades implements Initializable, ControlledScreen {

    ControllerScreens controllerScreens;

    @Override
    public void setScreenParent(ControllerScreens screenPage) {
        controllerScreens = screenPage;
    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {

    }
}
