import { Injectable } from '@angular/core';

export interface StorageConfig {
  prefix?: string;
}

@Injectable()
export abstract class BaseStorage implements Storage {
  constructor(private storage: Storage, private config: StorageConfig) { }

  get length(): number {
    return this.storage.length;
  }
  
  getItem(key: string): string | null {
    return this.storage.getItem(this.prefixKey(key));
  }

  setItem(key: string, value: string): void {
    this.storage.setItem(this.prefixKey(key), value);
  }

  removeItem(key: string): void {
    this.storage.removeItem(this.prefixKey(key));
  }

  clear(): void {
    return this.storage.clear();
  }

  key(index: number): string | null {
    return this.storage.key(index);
  }

  private prefixKey(plainKey: string): string {
    if (this.config?.prefix) {
      return `[${this.config?.prefix}]${plainKey}`;
    }

    return plainKey;
  }
}
