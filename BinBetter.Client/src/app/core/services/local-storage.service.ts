import { Injectable, Inject } from '@angular/core';
import { BaseStorage, StorageConfig } from './base-storage.service';
import { WINDOW } from '../tokens/window.token';
import { STORAGE_CONFIG } from '../tokens/storage-config.token';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService extends BaseStorage {
  constructor(@Inject(WINDOW) window: Window, @Inject(STORAGE_CONFIG) config: StorageConfig) {
    super(window.localStorage, config);
  }
}
