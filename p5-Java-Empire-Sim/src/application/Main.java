package application;
	
import javafx.application.Application;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.stage.Stage;


public class Main extends Application {

    public static  String menu1ID = "background";
    public static String screen1File = "menu.fxml";
    public static String menu2ID = "menu";
    public static String screen2File = "gameView.fxml";
    public static String menu3ID = "up";
    public static String screen3File = "upgrades.fxml";



	@Override
	public void start(Stage primaryStage) {
		try {
			ControllerScreens mainController = new ControllerScreens();
            mainController.loadScreen(Main.menu1ID, Main.screen1File);
            mainController.loadScreen(Main.menu2ID, Main.screen2File);
            mainController.loadScreen(Main.menu3ID,Main.screen3File);

            mainController.setScreen(Main.menu1ID);
            Group group = new Group();
            group.getChildren().addAll(mainController);
            primaryStage.setScene(new Scene(group));
            primaryStage.show();

		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	public static void main(String[] args) {
		launch(args);
	}
}
