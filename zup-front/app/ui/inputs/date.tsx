import {Button, DateInput, DatePicker, DateSegment, FieldError, Group, Label, Text, type DatePickerProps, type DateValue, type ValidationResult} from 'react-aria-components';
export interface DateSelectorProps<T extends DateValue> extends DatePickerProps<T> {
    label?: string;
    description?: string;
    errorMessage?: string | ((validation: ValidationResult) => string);
}
export default function DateSelector<T extends DateValue>(
    {label, description, errorMessage, ...props}: DateSelectorProps<T>
) {
    return (
        <DatePicker {...props}>
            <Label>{label}</Label>
            <Group className={" flex-wrap flex"}>
                <DateInput className={"bg-blue-500 w-32"}>
                    {(segment) => <DateSegment className={"flex flex-wrap w-32"} segment={segment}/>}
                </DateInput>
                <Button className={"flex flex-wrap w-32"}>▼</Button>
            </Group>
            {description && <Text slot="description">{description}</Text>}
            <FieldError>{errorMessage}</FieldError>
        </DatePicker>
    );
}