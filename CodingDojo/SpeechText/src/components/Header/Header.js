import { ContainerHeader, Logo } from "./StyleHEader";

export function HEadeer() {
    return(
        <ContainerHeader>
            <Logo source={require("../../assets/logotipo.png")}/>
        </ContainerHeader>
    )
}