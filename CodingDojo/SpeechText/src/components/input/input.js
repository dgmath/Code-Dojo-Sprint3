import { ContainerSub } from "./styleInput";

export function InputHigh({
    placeholder,
    fieldValue,
    onChangeText,
    keyboardType,
    maxLenght,
    editable,
    multiline = true
}) {
    return (
        <ContainerSub 
            multiline ={multiline}
            value = {fieldValue}
            onChangeText = {onChangeText}
        />
    )
}