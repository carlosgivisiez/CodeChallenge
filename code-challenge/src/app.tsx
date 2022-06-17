import { useOidcAccessToken } from "@axa-fr/react-oidc";
import { HttpTransportType, HubConnectionBuilder } from "@microsoft/signalr";
import { useMemo } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Lounge } from "./lounge/lounge";
import { Room } from "./room/room";
import { WebsocketProvider } from "./signalr-context/signalr-context";

export const App = () => {
    const accessToken = useOidcAccessToken();
    const hubConnection = useMemo(() => {
      return new HubConnectionBuilder()
      .withUrl("https://localhost:7172/hub", {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,            
        accessTokenFactory: () => accessToken.accessToken
      }).build()
    }, [accessToken.accessToken]);

    return (
        <WebsocketProvider connection={hubConnection}>
          <BrowserRouter>
            <Routes>
              <Route path="/" element={<Lounge />} />
              <Route path="/room/:id" element={<Room />} />
            </Routes>
          </BrowserRouter>
        </WebsocketProvider>
    );
}