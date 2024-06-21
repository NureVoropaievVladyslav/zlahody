export interface ChatThumbnailResponse {
    chatId: string;
    contactName: string;
}

export interface MessageResponse {
    content: string;
    senderName: string;
    senderEmail: string;
    createdAt: Date;
}