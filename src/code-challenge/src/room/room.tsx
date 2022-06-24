import { Field, Form, Formik } from "formik";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Loader } from "../loader/loader";
import { useWebsocket } from "../signalr-context/signalr-context";
import { RoomData } from "./room-data";
import "./room.scss";

export const Room = () => {
    const websocket = useWebsocket();
    const { id } = useParams<{ id: string }>();
    const [room, setRoom] = useState<Promise<RoomData>>();

    useEffect(() => {
        setRoom(
            Promise.all([
                websocket.send("JoinRoom", id),
                websocket.invoke<RoomData>("GetRoom", id)
            ]).then(([, room]) => room)
        );

        websocket.on(
            "messagesUpdated", () => {
                setRoom(websocket.invoke<RoomData>("GetRoom", id))
            });

        return () => {
            websocket.send("LeaveRoom", id);
        }
    }, [id, websocket]);

    return (
        <Loader promise={room} implicit={true}>
            {room => room && (
                <div className="room">
                    <h3>Chat room: {room.name}</h3>
                    <div className="messages">
                        {room.messages
                            .sort((prev, curr) => new Date(prev.dateTime).getTime() - new Date(curr.dateTime).getTime())
                            .map(m => (
                                <p key={m.id}>{m.content}</p>
                            ))}
                    </div>
                    <Formik
                        initialValues={{
                            message: ""
                        }}
                        onSubmit={(values, helpers) => {
                            websocket.send("SendMessage", values.message, id);

                            helpers.resetForm();
                        }}
                    >
                        <Form className="message-sending">
                            <Field name="message" />
                            <button type="submit">Send</button>
                        </Form>
                    </Formik>
                </div>
            )}
        </Loader>
    );
}