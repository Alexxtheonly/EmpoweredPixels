import { Observable, from } from 'rxjs';
import { TranslateLoader } from '@ngx-translate/core';

export class WebpackTranslateLoader implements TranslateLoader
{
    getTranslation(lang: string): Observable<any>
    {
        return from(import(`../../assets/i18n/${lang}.json`));
    }
}
