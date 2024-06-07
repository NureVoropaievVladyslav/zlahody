export interface Organization {
    id: string;
    name: string;
    numberOfMembers: number;
}

export interface Application {
    organisationId: string;
    isAccepted: boolean;
}

export interface OrganizationApplication {
    volunteerName: string;
    volunteerId: string;
    isAccepted: boolean;
}