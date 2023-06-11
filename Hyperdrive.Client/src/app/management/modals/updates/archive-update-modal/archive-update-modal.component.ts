import {
  Component,
  OnInit,
  Inject
} from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import {
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';

import { BinaryUpdateArchive } from './../../../../../viewmodels/binary/binaryupdatearchive';

import { ViewArchive } from './../../../../../viewmodels/views/viewarchive';

import { ViewApplicationUser } from '../../../../../viewmodels/views/viewapplicationuser';

import { ArchiveService } from './../../../../../services/archive.service';

import { BinaryService } from './../../../../../services/binary.service';

import { TextAppVariants } from './../../../../../variants/text.app.variants';

import { TimeAppVariants } from './../../../../../variants/time.app.variants';

@Component({
  selector: 'app-archive-update-modal',
  templateUrl: './archive-update-modal.component.html',
  styleUrls: ['./archive-update-modal.component.scss']
})
export class ArchiveUpdateModalComponent implements OnInit {

  public formGroup!: FormGroup;

  public User!: ViewApplicationUser;

  // Constructor
  constructor(
    private binaryService: BinaryService,
    private archiveService: ArchiveService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<ArchiveUpdateModalComponent>,
    private matSnackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: ViewArchive) { }


  // Life Cicle
  ngOnInit() {
    this.GetLocalUser();
    this.CreateForm();
  }

  // Form
  CreateForm() {
    this.formGroup = this.formBuilder.group({
      Id: [this.data.Id, [Validators.required]],
      Data: [TextAppVariants.AppEmptyCoreText,
      [Validators.required]],
      ApplicationUserId: [this.User.Id, [Validators.required]]
    });
  }

  // Form Actions
  async onSubmit(viewModel: BinaryUpdateArchive) {

    let archive = await this.archiveService.UpdateArchive(await this.binaryService.EncodeUpdateArchive(viewModel));

    if (archive) {
      this.matSnackBar.open(
        TextAppVariants.AppOperationSuccessCoreText,
        TextAppVariants.AppOkButtonText,
        { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
    }

    this.dialogRef.close();
  }

  async onDelete(viewModel: BinaryUpdateArchive) {
    await this.archiveService.RemoveArchiveById(viewModel.Id);

    this.matSnackBar.open(
      TextAppVariants.AppOperationSuccessCoreText,
      TextAppVariants.AppOkButtonText,
      { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

    this.dialogRef.close();

  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(localStorage.getItem('User')!);
  }
}
