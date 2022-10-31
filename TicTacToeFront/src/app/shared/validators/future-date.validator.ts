import { AbstractControl } from "@angular/forms";

export function validateFutureDate(control: AbstractControl) {
  const pickedDate = control.value as Date
  var today = new Date()
  today.setHours(0, 0, 0, 0)
  if (pickedDate <= today) {
    return { invalidDate: true };
  }
  return null;
}