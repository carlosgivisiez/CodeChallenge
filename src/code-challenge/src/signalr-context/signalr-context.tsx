import signalR, { HubConnectionState } from "@microsoft/signalr";
import React, { useEffect, useState } from "react";
import { Loader } from "../loader/loader";

const WebsocketContext = React.createContext<signalR.HubConnection | undefined>(undefined);

interface WebsocketProviderProps {
    connection?: signalR.HubConnection;
    children: React.ReactNode;
}

export const WebsocketProvider = (props: WebsocketProviderProps) => {
    const [promise, setPromise] = useState<Promise<void>>();

    useEffect(() => {
        if (props.connection?.state === HubConnectionState.Disconnected) {
            setPromise(props.connection.start());
        }
    }, [props.connection?.state]);

    return (
        <Loader promise={promise}>
            {() => (
                <WebsocketContext.Provider value={props.connection}>
                    {props.children}
                </WebsocketContext.Provider>
            )}
        </Loader>
    )
}

export const useWebsocket = () => {
    if (!WebsocketContext) {
        throw new Error("Websocket provider not identified");
    }

    return React.useContext(WebsocketContext)!;
}