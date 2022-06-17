import { HubConnection } from "@microsoft/signalr";
import { Field, Form, Formik } from "formik";
import styles from "./create-room.module.scss";

interface CreateRoomProps {
    websocket: HubConnection;
    onSubmit: () => void;
}

export const CreateRoom = (props: CreateRoomProps) => {
    return (
        <Formik
            initialValues={{
                name: ""
            }}
            onSubmit={values => {
                props.websocket.send("CreateRoom", values.name);
                props.onSubmit();
            }}
        >
            <Form className={styles["create-room"]}>
                <Field name="name" type="text" />
                <button type="submit">Create</button>
            </Form>
        </Formik>
    );
}