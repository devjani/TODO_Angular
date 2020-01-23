import { Injectable } from '@angular/core';
import { Subject, Subscription, Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';

interface Message {
    type: string;
    payload: any;
}

export type MessageCallback = (payload: any) => void;

@Injectable()
export class EventService {
    private handler = new Subject<Message>();

    broadcast(type: string, payload: any) {
        this.handler.next({ type, payload });
    }

    subscribe(type: string, callback: MessageCallback): Subscription {
        return this.handler.pipe(filter((message: any) => message.type === type))
            .pipe(map((message: any) => message.payload)).subscribe(callback);
    }

    getEvents(): Observable<any> {
        return this.handler.asObservable();
    }
}
