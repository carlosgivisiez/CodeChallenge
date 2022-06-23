import { mdiCurrentAc, mdiPlus } from "@mdi/js";
import Icon from "@mdi/react";
import React, { useEffect, useState } from "react";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { Loader } from "../loader/loader";
import { useWebsocket } from "../signalr-context/signalr-context";
import { CreateRoom } from "./create-room/create-room";
import "./lounge.scss";
import { RoomCard } from "./room-card/room-card";
import { RoomSummary } from "./room-summary";

const ReactSwal = withReactContent(Swal)

export const Lounge = () => {      
    const websocket = useWebsocket();
    const [rooms, setRooms] = useState<Promise<Array<RoomSummary>>>();

    useEffect(() => {
        setRooms(websocket.invoke<Array<RoomSummary>>("GetRooms"));

        websocket.on(
            "roomsUpdated", () => {
                setRooms(websocket.invoke<Array<RoomSummary>>("GetRooms"))
            });
    }, [websocket]);
    
    return (
        <div className="lounge">
            <Loader promise={rooms}>
                {rooms => rooms && (
                    <div className="all-rooms">
                        {rooms
                            .sort((previous, current) => current.membersCount - previous.membersCount)
                            .map(r => (
                                <RoomCard key={r.id} roomSummary={r} />
                            ))}
                        <div 
                            className="add-room" 
                            onClick={async () => {
                                await ReactSwal.fire({
                                    title: "Create room", 
                                    html: <CreateRoom websocket={websocket} onSubmit={() => ReactSwal.close()} />,
                                    showConfirmButton: false,
                                });
                            }} 
                        >
                            <Icon path={mdiPlus} size={3} color="#242F9B" />
                            <p>Add room</p>
                        </div>
                    </div>
                )}
            </Loader>
        </div>
    );
}
