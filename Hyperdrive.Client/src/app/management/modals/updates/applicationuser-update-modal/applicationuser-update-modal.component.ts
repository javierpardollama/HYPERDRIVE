import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';

import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { ViewApplicationUser } from '../../../../../viewmodels/views/viewapplicationuser';

import { UpdateApplicationUser } from '../../../../../viewmodels/updates/updateapplicationuser';

import { ApplicationUserService } from '../../../../../services/applicationuser.service';

import { ApplicationRoleService } from '../../../../../services/applicationrole.service';

import { TextAppVariants } from '../../../../../variants/text.app.variants';

import { TimeAppVariants } from '../../../../../variants/time.app.variants';
import { ViewCatalog } from "../../../../../viewmodels/views/viewcatalog";
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-applicationuser-update-modal',
    templateUrl: './applicationuser-update-modal.component.html',
    styleUrls: ['./applicationuser-update-modal.component.scss'],
    imports: [
        MatDialogModule,
        MatButtonModule,
        FormsModule,
        MatTooltipModule,
        MatSnackBarModule,
        MatSelectModule,
        MatInputModule,
        MatFormFieldModule,
        ReactiveFormsModule
    ]
})
export class ApplicationUserUpdateModalComponent implements OnInit {
    // DI
    private applicationuserService = inject(ApplicationUserService);
    private applicationroleService = inject(ApplicationRoleService);
    private secureStorageService = inject(SecureStorageService);
    private formBuilder = inject(FormBuilder);
    dialogRef = inject<MatDialogRef<ApplicationUserUpdateModalComponent>>(MatDialogRef);
    private matSnackBar = inject(MatSnackBar);
    data = inject<ViewApplicationUser>(MAT_DIALOG_DATA);

    public User?: ViewApplicationUser;

    public formGroup!: FormGroup;

    public applicationroles: ViewCatalog[] = [];

    // Constructor
    constructor() {
    }

    // Life Cicle
    async ngOnInit(): Promise<void> {
        this.CreateForm();
        await this.FindAllApplicationRole();
        await this.GetLocalUser();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            Id: new FormControl<number>(this.data.Id, [
                Validators.required
            ]),
            ApplicationRolesId: new FormControl<number[]>(this.data.ApplicationRoles.map(({ Id }) => Id), [
                Validators.required]),
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id, [
                Validators.required
            ])
        });
    }

    // Form Actions
    async onSubmit(viewModel: UpdateApplicationUser): Promise<void> {
        let applicationuser = await this.applicationuserService.UpdateApplicationUser(viewModel);

        if (applicationuser) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
        }

        this.dialogRef.close();
    }

    async onDelete(viewModel: UpdateApplicationUser): Promise<void> {
        await this.applicationuserService.RemoveApplicationUserById(viewModel.Id);

        this.matSnackBar.open(
            TextAppVariants.AppOperationSuccessCoreText,
            TextAppVariants.AppOkButtonText,
            { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

        this.dialogRef.close();

    }

    // Get Data from Service
    public async FindAllApplicationRole(): Promise<void> {
        this.applicationroles = await this.applicationroleService.FindAllApplicationRole();
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);;
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }
}
