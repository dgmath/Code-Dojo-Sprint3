import { useEffect, useState } from "react";
import { HEadeer } from "../components/Header/Header";
import { InputHigh } from "../components/input/input";
import { ContainerTItle, Title, UnderTitle } from "../components/titles/StyleTitles";
import { Container } from "./StyleHome";
import { ButtonAudio, ButtonConvert, ButtonText, ContainerButton, ContainerButtonPlay, Line, PlayButton } from "../components/Buttons/StyleButton";
import { FontAwesome } from '@expo/vector-icons';
import { AntDesign } from '@expo/vector-icons';
import api from "../srvices/Services";
import SoundPlayer from 'react-native-sound-player'

export function Home() {

    const [text, setText] = useState('');
    const [audio, setAudio] = useState('');






    async function PostAudio() {

        await api.post('/AzureSpeechService/TextoEmAudio',
            { text: text }).then(response => {

                console.log(123);

                alert("sucesso");

                console.log(response.data)
                setAudio(response.data)
                console.log(audio);

            })


            

    }

    function TocarAudio() {
        try {
            // or play from url
            SoundPlayer.playUrl(`'${audio}'`)
        } catch (e) {
            console.log(`cannot play the sound file`, e)
        }
    }

    useEffect(() => {
        if (audio != null) {
            TocarAudio()
        }
    },[audio])
    return (


        <Container>
            <HEadeer />

            <ContainerTItle>
                <Title>Converta texto para fala</Title>
                <UnderTitle>Ou fala para texto</UnderTitle>
            </ContainerTItle>

            <InputHigh
                fieldValue={text}
                multiline={true}
                onChangeText={(x) => setText(x)}
            />

            <ContainerButton>
                <ButtonConvert
                    onPress={() => PostAudio()}
                >
                    <ButtonText>Converter</ButtonText>
                </ButtonConvert>
                <ButtonAudio><FontAwesome name="microphone" size={24} color="black" /></ButtonAudio>
            </ContainerButton>

            <ContainerButtonPlay>

                <PlayButton onPress={() => TocarAudio()}>
                    <AntDesign name="caretright" size={24} color="black" />


                </PlayButton>
                <Line />
            </ContainerButtonPlay>
        </Container>

    )
}