import { InjectionToken } from '@angular/core';
import { StorageConfig } from '../services/base-storage.service';

export const STORAGE_CONFIG = new InjectionToken<StorageConfig>('Storage options', {
  factory: () => ({
    prefix: 'bb'
  })
});
