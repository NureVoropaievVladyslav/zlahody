export interface Resource {
    id: string;
    title: string;
    type: ResourceType;
}

export type ResourceType = 'Shelter' | 'Medical' | 'Food' | 'TemporaryHousing' | 'Other';

function mapResourceTypeToString(type: number): ResourceType | undefined {
    switch (type) {
      case 0:
        return 'Shelter';
      case 1:
        return 'Medical';
      case 2:
        return 'Food';
      case 3:
        return 'TemporaryHousing';
      case 3:
        return 'Other';
      default:
        return undefined;
    }
  }