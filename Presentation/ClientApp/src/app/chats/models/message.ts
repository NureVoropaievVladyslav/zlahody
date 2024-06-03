export interface MessageSendRequest {
    chatId: string;
    content: string;
}

export interface Message {
    content: string;
    senderEmail: string;
    createdAt: Date;
}