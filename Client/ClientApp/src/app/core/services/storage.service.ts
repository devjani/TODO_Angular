import { Injectable } from '@angular/core';

@Injectable()
export class StorageService {

  constructor() { }

  setValue(key: string, value: string | number | object | boolean) {
    let storageValue = '';
    if (typeof value === 'object') {
      storageValue = JSON.stringify(value);
    } else {
      storageValue = value.toString();
    }
    localStorage.setItem(key, storageValue);
  }

  getValue(key) {
    return localStorage.getItem(key);
  }

  removeValue(key) {
    localStorage.removeItem(key);
  }

  removeAll() {
    localStorage.clear();
  }


}
