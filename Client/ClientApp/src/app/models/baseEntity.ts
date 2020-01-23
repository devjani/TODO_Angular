export class BaseEntity {
    Id?: string;
    CreatedAt?: Date | string;
    UpdatedAt?: Date | string;
    IsDeleted?: boolean;
    DeletedAt?: Date | string;
    CreatedBy: string;
    UpdatedBy: string;
}
