import { BaseEntity } from './baseEntity';

export class Todo extends BaseEntity {
    Name: string;
    Description: string;
    IsActive: boolean;
    TodoId: string;
}
