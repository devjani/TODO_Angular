import { NgModule } from '@angular/core';
import {
    MatButtonModule, MatInputModule, MatFormFieldModule,
    MatCheckboxModule,
    MatSelectModule,
    MatOptionModule, MatRadioModule,
    MatDatepickerModule,
    MatDialogModule,
    MatSlideToggleModule,
    MatAutocompleteModule,
    MatTabsModule,
    MatMenuModule,
    MatExpansionModule,
    MatIconModule,
    MatListModule, MatSidenavModule,
    MatTableModule,
    MatCardModule,
    MatToolbarModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    MatSortModule,
    MatNativeDateModule,
    MatChipInput,
    MatChipsModule,
    MatStepperModule,
    MatProgressBarModule
} from '@angular/material';

@NgModule({
    imports: [MatButtonModule, MatInputModule, MatFormFieldModule,
        MatCheckboxModule, MatSelectModule,
        MatOptionModule, MatRadioModule,
        MatDialogModule, MatSlideToggleModule,
        MatDatepickerModule,
        MatAutocompleteModule, MatTabsModule, MatMenuModule, MatExpansionModule, MatIconModule,
        MatListModule, MatSidenavModule, MatTableModule, MatCardModule, MatToolbarModule, MatProgressSpinnerModule,
        MatPaginatorModule, MatSortModule, MatNativeDateModule, MatInputModule, MatChipsModule,
        MatStepperModule, MatProgressBarModule,
        MatCardModule],
    exports: [MatButtonModule, MatInputModule, MatFormFieldModule,
        MatCheckboxModule, MatSelectModule,
        MatOptionModule, MatRadioModule, MatDialogModule,
        MatDatepickerModule, MatSlideToggleModule,
        MatAutocompleteModule, MatTabsModule, MatMenuModule, MatExpansionModule, MatIconModule,
        MatListModule, MatSidenavModule, MatTableModule, MatCardModule, MatToolbarModule, MatProgressSpinnerModule,
        MatPaginatorModule, MatSortModule, MatNativeDateModule, MatInputModule, MatChipsModule,
        MatStepperModule, MatProgressBarModule,
        MatCardModule
    ]
})
export class MaterialModule { }
