export interface RoomData {
    id: string;
    name: string;
    messages: Array<Message>;
    membersIds: Array<string>;
}

export interface Message {
    id: string;
    content: string;
    ownerId: string;
    dateTime: string;
    quoteeMessageId: string;
}