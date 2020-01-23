import { AbstractControl } from '@angular/forms';
import { isNullOrUndefined } from 'util';

export enum UserDetailsTabs {
  Details
}

export enum ContainerDetailsTabs {
  Details = 0,
  Equipment = 1,
  Projects = 2,
  WorkOrder = 3,
  Notes = 4
}
export enum ArticleDetailsTabs {
  Details = 0,
  TypeTolerance = 1,
  CheckList = 2,
  Notes = 3

}

export enum CalibrationDetailsTabs {
  Details = 0,
  CalibrationPoint = 1,
  Notes = 2
}

export enum ProcedureDetailsTabs {
  Details = 0,
  Content = 1,
  Notes = 2
}

export enum EquipmentDetailsTabs {
  Details = 0,
  CustomerInfo = 1,
  CustomTolerance = 2,
  Container = 3,
  Projects = 4,
  CheckList = 5,
  Certificates = 6,
  Notes = 7,
  Attachments = 8
}

export enum ReferenceEquipmentDetailsTabs {
  Details = 0,
  Adjustments = 1,
  CalibrationHistory = 2,
  Certificates = 3,
  Notes = 4,
  Attachments = 5
}


export enum CustomerDetailsTabs {
  Details = 0,
  ContactPerson = 1,
  Equipments = 2,
  Container = 3,
  CalibrationsWO = 4,
  Certificates = 5,
  CheckList = 6,
  Notes = 7
}

export enum ProjectDetailsTabs {
  Details = 0,
  WorkOrders = 1,
  Equipments = 2,
  Container = 3,
  ContactPerson = 4,
  Certificates = 5,
  Notes = 6
}

export enum WorkOrderDetailsTabs {
  Details = 0,
  SuggestedCalibration = 1,
  ExecutedCalibration = 2,
  Approval = 3,
  Certificates = 4,
  AssociatedWorkOrder = 5,
  Equipment = 6,
  Notes = 7
}

export enum CustomerEquipmentDetailsTabs {
  Details = 0,
  CustomTolerance = 1,
  Certificates = 2,
  Notes = 3
}

export enum CustomerLoginDetailsTabs {
  Details = 0,
  ContactPerson = 1,
  Equipments = 2,
  Certificates = 3,
  Notes = 4
}
export enum Status {
  Active = 1,
  Inactive = 2
}
export enum ContainerStatus {
  Empty = 0,
  WaitingToProcessed = 1,
  AssignedToProject = 2,
  ProjectUnderCalibration = 3,
  CalibrationCompleted = 4,
  EquipmentToBeShipped = 5
}

export enum Languages {
  English = 'en',
  Dutch = 'nl',
  French = 'fr'
}
export enum CalibrationType {
  Standard = 1,
  Selective = 2
}

export const StatusDescription = new Map<number, string>([
  [Status.Active, 'Active'],
  [Status.Inactive, 'InActive']
]);
export const CalibrationTypeDescription = new Map<number, string>([
  [CalibrationType.Standard, 'Standard'],
  [CalibrationType.Selective, 'Selective']
]);
export const CurrentStatusDescription = new Map<number, string>([
  [ContainerStatus.Empty, 'Empty'],
  [ContainerStatus.WaitingToProcessed, 'WaitingToProcessed'],
  [ContainerStatus.AssignedToProject, 'AssignedToProject'],
  [ContainerStatus.ProjectUnderCalibration, 'ProjectUnderCalibration'],
  [ContainerStatus.CalibrationCompleted, 'CalibrationCompleted'],
  [ContainerStatus.EquipmentToBeShipped, 'EquipmentToBeShipped']
]);
export const YesNoDescription = new Map<string, boolean>([
  ['Yes', true],
  ['No', false]
]);

export enum EquipmentType {
  Master = 1,
  Slave = 2,
  Adapter = 3
}

export const EquipmentTypeDescription = new Map<number, string>([
  [EquipmentType.Master, 'Master'],
  [EquipmentType.Slave, 'Slave'],
  [EquipmentType.Adapter, 'Adapter']
]);

export enum EquipmentStatus {
  Operational = 1,
  Destroyed = 2,
  PermanentlyOOT = 3
}
export enum CheckListType {
  Optional = 1,
  Mandatory = 2,
}
export const EquipmentStatusDescription = new Map<number, string>([
  [EquipmentStatus.Operational, 'Operational'],
  [EquipmentStatus.Destroyed, 'Destroyed'],
  [EquipmentStatus.PermanentlyOOT, 'Permanently OOT']
]);

export const CheckListTypeDescription = new Map<number, string>([
  [CheckListType.Optional, 'Optional'],
  [CheckListType.Mandatory, 'Mandatory'],
]);

export enum CalibrationCyclePeriod {
  Months = 1,
  Years = 2
}
export const CalibrationCyclePeriodDescription = new Map<number, string>([
  [CalibrationCyclePeriod.Months, 'Months'],
  [CalibrationCyclePeriod.Years, 'Years'],
]);

export enum CustomerType {
  Existing = 1,
  New = 2
}
export const LanguageDescription = new Map<string, string>([
  ['D', 'nl'],
  ['F', 'fr'],
  ['E', 'en']
]);

export enum RoleType {
  Manager = 1,
  Technician = 2,
  Other = 3,
  SuperAdmin = 4,
  Customer = 5
}


export const validFileExtension = {
  JPEG: 'jpeg',
  JPG: 'jpg',
  PNG: 'png',
  DOC: 'doc',
  DOCX: 'docx',
  PDF: 'pdf',
  OST: 'ost',
  PST: 'pst',
  XLS: 'xls',
  XLSX: 'xlsx'
};
export class Common {
  static fileUploadValidator(abstractCtrl: AbstractControl) {
    const fileUploadFile = abstractCtrl.get('fileUpload');
    if (!isNullOrUndefined(fileUploadFile.value) && fileUploadFile.value !== '') {
      const fileExtension: string = fileUploadFile.value.replace(/^.*\./, '').toLowerCase();
      if (fileExtension !== validFileExtension.JPG &&
        fileExtension !== validFileExtension.JPEG &&
        fileExtension !== validFileExtension.PNG &&
        fileExtension !== validFileExtension.DOC &&
        fileExtension !== validFileExtension.DOCX &&
        fileExtension !== validFileExtension.PDF &&
        fileExtension !== validFileExtension.OST &&
        fileExtension !== validFileExtension.PST) {
        fileUploadFile.setErrors({ fileFormatInvalid: true });
      } else {
        return null;
      }
    }
  }

  static excelUploadValidator(abstractCtrl: AbstractControl) {
    const fileUploadFile = abstractCtrl.get('fileUpload');
    if (!isNullOrUndefined(fileUploadFile.value) && fileUploadFile.value !== '') {
      const fileExtension: string = fileUploadFile.value.replace(/^.*\./, '').toLowerCase();
      if (fileExtension !== validFileExtension.XLS &&
        fileExtension !== validFileExtension.XLSX) {
        fileUploadFile.setErrors({ fileFormatInvalid: true });
      } else {
        return null;
      }
    }
  }
  static imageUploadValidator(abstractCtrl: AbstractControl) {
    const fileUploadFile = abstractCtrl.get('fileUpload');
    if (!isNullOrUndefined(fileUploadFile.value) && fileUploadFile.value !== '') {
      const fileExtension: string = fileUploadFile.value.replace(/^.*\./, '').toLowerCase();
      if (fileExtension !== validFileExtension.JPG &&
        fileExtension !== validFileExtension.JPEG &&
        fileExtension !== validFileExtension.PNG) {
        fileUploadFile.setErrors({ fileFormatInvalid: true });
      } else {
        return null;
      }
    }
  }
}
export const getLanguage = (language) => {
  switch (language) {
    case 'E':
      return 'en';
    case 'F':
      return 'fr';
    case 'D':
      return 'nl';
  }
};
export const numberValidator = (
  control: AbstractControl
): { [key: string]: any } | null => {
  const valid = /^-?[0-9]\d{0,9}(\.\d{1,4})?%?$/.test(control.value);
  // \b\d[\d,.\d]*\b
  // ^-?[0-9]\d*(\.\d+)?$
  // ^(1000|\d)(\.\d{1,2})?$
  return valid
    ? null
    : { invalidNumber: { valid: false, value: control.value } };
};

export const decimalNumberValidator = (
  control: AbstractControl
): { [key: string]: any } | null => {
  const valid = /^([0-9]\d{0,9}(\.\d{1,4})?)?$/.test(control.value);
  // \b\d[\d,.\d]*\b
  // ^-?[0-9]\d*(\.\d+)?$
  // ^(1000|\d)(\.\d{1,2})?$
  return valid
    ? null
    : { invalidNumber: { valid: false, value: control.value } };
};
export const getLanguageCode = (languageCode) => {
  switch (languageCode) {
    case 'en':
      return 'E';
    case 'fr':
      return 'F';
    case 'nl':
      return 'D';
  }
};

export enum InternalExternalType {
  Internal = 1,
  External = 2,
  InternalExternal = 3
}

export const InternalExternalTypeDescription = new Map<number, string>([
  [InternalExternalType.Internal, 'Internal'],
  [InternalExternalType.External, 'External'],
  [InternalExternalType.InternalExternal, 'InternalExternal']

]);

export const CustomToleranceStatus = new Map<string, boolean>([
  ['Applicable', true],
  ['Not Applicable', false]
]);

export enum GenderType {
  Female = 1,
  Male = 2
}

export const GenderTypeDesc = new Map<number, string>([
  [GenderType.Female, 'Female'],
  [GenderType.Male, 'Male']
]);


export enum CertificateCheckType {
  CertificatePerWorkOrder = 1,
  CertificatePerProject = 2,
}

export const CertificateCheckTypeDescription = new Map<number, string>([
  [CertificateCheckType.CertificatePerWorkOrder, 'CertificatePerWorkOrder'],
  [CertificateCheckType.CertificatePerProject, 'CertificatePerProject']
]);

export enum ProjectStatus {
  Pending = 1,
  InCalibration = 2,
  OnHold = 3,
  ReplacementOfDevice = 4,
  CalibrationCompleted = 5,
  Closed = 6
}

export const ProjectStatusDescription = new Map<number, string>([
  [ProjectStatus.Pending, 'Pending'],
  [ProjectStatus.InCalibration, 'InCalibration'],
  [ProjectStatus.OnHold, 'OnHold'],
  [ProjectStatus.ReplacementOfDevice, 'ReplacementOfDevice'],
  [ProjectStatus.CalibrationCompleted, 'CalibrationCompleted'],
  [ProjectStatus.Closed, 'Closed'],
]);

export enum WorkOrderStatus {
  Pending = 1,
  InCalibration = 2,
  OnHold = 3,
  ManagerApprovalPending = 4,
  DeviceUnderAdjustment = 5,
  CalibrationCompleted = 6,
  Cancelled = 7
}

export const WorkOrderStatusDescription = new Map<number, string>([
  [WorkOrderStatus.Pending, 'Pending'],
  [WorkOrderStatus.InCalibration, 'InCalibration'],
  [WorkOrderStatus.OnHold, 'OnHold'],
  [WorkOrderStatus.ManagerApprovalPending, 'ManagerApprovalPending'],
  [WorkOrderStatus.DeviceUnderAdjustment, 'DeviceUnderAdjustment'],
  [WorkOrderStatus.CalibrationCompleted, 'CalibrationCompleted'],
  [WorkOrderStatus.Cancelled, 'Cancelled']
]);



export enum ExecutionStepType {
  Initial = 1,
  AfterAdjustment = 2,
}

export const ExecutionStepTypeDescription = new Map<number, string>([
  [ExecutionStepType.Initial, 'Initial'],
  [ExecutionStepType.AfterAdjustment, 'AfterAdjustment'],
]);


export enum CalibrationObjectMasterEnum {
  Temperature = 1,
  Humidity = 2,
  AirPressure = 3
}

export enum CalibrationStatus {
  Done = 1,
  Pending = 2
}

export const CalibrationStatusDesc = new Map<number, string>([
  [CalibrationStatus.Pending, 'Pending'],
  [CalibrationStatus.Done, 'Done']
]);
