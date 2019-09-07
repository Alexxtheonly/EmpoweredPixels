import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class UserFeedbackService
{
  private toastSettings = {
    closeButton: true,
  };

  constructor(private toastService: ToastrService, private translateService: TranslateService)
  {
  }

  public success(text: string): void
  {
    if (!text || text.length === 0)
    {
      return;
    }

    this.toastService.success(text, '', this.toastSettings);
  }

  public error(error: any): void
  {
    if (!error.error.code)
    {
      return;
    }

    const errorcode = Number(error.error.code);

    this.toastService.error(this.translateService.instant('errorcodes.' + errorcode), '', this.toastSettings);
  }
}
