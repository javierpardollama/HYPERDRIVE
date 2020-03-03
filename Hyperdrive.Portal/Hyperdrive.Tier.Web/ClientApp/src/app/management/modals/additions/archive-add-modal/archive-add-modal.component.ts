import {
  Component,
  OnInit
} from '@angular/core';

import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import {
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';

import { BinaryAddArchive } from './../../../../../viewmodels/binary/binaryaddarchive';

import { ViewApplicationUser } from './../../../../../viewmodels/views/viewapplicationuser';

import { ArchiveService } from './../../../../../services/archive.service';

import { BinaryService } from './../../../../../services/binary.service';

import { TextAppVariants } from './../../../../../variants/text.app.variants';

import { TimeAppVariants } from './../../../../../variants/time.app.variants';

@Component({
  selector: 'app-archive-add-modal',
  templateUrl: './archive-add-modal.component.html',
  styleUrls: ['./archive-add-modal.component.css']
})
export class ArchiveAddModalComponent implements OnInit {

  public formGroup: FormGroup;

  public User: ViewApplicationUser;

  // Constructor
  constructor(
    private binaryService: BinaryService,
    private archiveService: ArchiveService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<ArchiveAddModalComponent>,
    private matSnackBar: MatSnackBar) { }


  // Life Cicle
  ngOnInit() {
    this.GetLocalUser();
    this.CreateForm();
  }

  // Form
  CreateForm() {
    this.formGroup = this.formBuilder.group({
      Name: [TextAppVariants.AppEmptyCoreText,
      [Validators.required]],
      Data: [TextAppVariants.AppEmptyCoreText,
      [Validators.required]],
      By: [this.User, [Validators.required]]
    });
  }

  // Form Actions
  onSubmit(viewModel: BinaryAddArchive) {

    this.archiveService.AddArchive(this.binaryService.EncodeAddArchive(viewModel)).subscribe(viewArchive => {

      if (viewArchive !== undefined) {
        this.matSnackBar.open(
          TextAppVariants.AppOperationSuccessCoreText,
          TextAppVariants.AppOkButtonText,
          { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
      }

      this.dialogRef.close();
    });
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(localStorage.getItem('User'));
  }
}
