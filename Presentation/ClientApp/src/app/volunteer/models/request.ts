export interface Request {
    content: string;
    id: string;
    requestType: RequestType;
    isApproved: boolean;
    victimId: string;
    createdAt: Date;
}

export enum RequestType {
    Food,
    Accommodation,
    'Information Request',
    Complaint,
    Feedback,
    'Psychological Support'
}