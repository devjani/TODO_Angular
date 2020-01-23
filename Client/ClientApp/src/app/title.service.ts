import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TitleService {

  // tslint:disable-next-line: variable-name
  private _backButton: boolean;
  // tslint:disable-next-line: variable-name
  private _headerTitle: string;
  // tslint:disable-next-line: variable-name
  private _link: string;

  public get headerTitle(): string {
    return this._headerTitle;
  }
  public set headerTitle(v: string) {
    this._headerTitle = v;
  }
  public get backButton(): boolean {
    return this._backButton;
  }
  public set backButton(v: boolean) {
    this._backButton = v;
  }
  public get link(): string {
    return this._link;
  }
  public set link(v: string) {
    this._link = v;
  }

  constructor() { }
}
