package application; /**
 * Created by borislavivanov on 9/22/15.
 */

import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.layout.VBox;
import javafx.stage.Modality;
import javafx.stage.Stage;

public class AlertBox extends Main{

    public static void display() {
        Stage primaryStage = new Stage();
        primaryStage.setTitle("Exit Box");
        primaryStage.initModality(Modality.APPLICATION_MODAL);

        Button yesButton = new Button("Yes");
        //TODO
       //yesButton.setOnAction(event -> primaryStage.close());
        Button noButton = new Button("No");
        noButton.setOnAction(event -> primaryStage.close());

        VBox vBox = new VBox(15);
        vBox.setMinHeight(100);
        vBox.setMinWidth(200);
        vBox.setAlignment(Pos.CENTER);
        vBox.getChildren().addAll(yesButton, noButton);

        primaryStage.setScene(new Scene(vBox));
        primaryStage.show();

    }
}
