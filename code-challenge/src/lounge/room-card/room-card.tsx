import { useState } from "react";
import { Link } from "react-router-dom";
import { RoomSummary } from "../room-summary";
import "./room-card.scss";

interface RoomCardProps {
    roomSummary: RoomSummary;
}

export const RoomCard = (props: RoomCardProps) => {
    const [flipped, setFlipped] = useState(false);

    return (
        <Link to={`/room/${props.roomSummary.id}`}>
            <div className="room-card" onMouseEnter={() => setFlipped(true)} onMouseLeave={() => setFlipped(false)}>
                {!flipped ?
                    <>
                        <h3>{props.roomSummary.name}</h3>
                        <p>{props.roomSummary.membersCount} members</p>
                    </>
                    :
                    <h3>Join</h3>}
            </div>
        </Link>
    );
}