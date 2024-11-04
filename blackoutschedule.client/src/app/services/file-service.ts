import { Injectable } from '@angular/core';
import { saveAs } from 'file-saver';
import { Observable } from 'rxjs';

import * as XLSX from 'xlsx';

@Injectable({
  providedIn: 'root',
})

export class FileService {

  saveArrayAsJsonFile(dataArray: any[], fileName: string): void {
    const json = JSON.stringify(dataArray, null, 2);
    const blob = new Blob([json], { type: 'application/json' });
    saveAs(blob, `${fileName}.json`);
  }

  readXLSXFile(file: File): Observable<string[]> {
    const reader = new FileReader();
    var values: string[] = new Array<string>();

    return new Observable(observer => {
      reader.onload = (e: any) => {
        const workbook = XLSX.read(e.target.result, { type: 'binary' });

        (workbook as any).Strings.forEach((row: any) => {
          if (row && row.h) {
            values.push(row.h as string);
          }
        });

        observer.next(values);
        observer.complete();
      };

      reader.readAsBinaryString(file);
    })
  }
}
