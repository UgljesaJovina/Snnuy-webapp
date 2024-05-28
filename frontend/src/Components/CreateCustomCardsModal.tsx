import { Dispatch, SetStateAction, useEffect, useRef, useState } from "react";
import { Dropdown, DropdownButton, DropdownContent, DropdownItem, DropdownList } from ".";
import { baseUrl } from "../utils/GlobalVariables";
import { useRecoilValue } from "recoil";
import { authAtom } from "../atoms";
import { TCustomCard } from "../types";

const CardCreateModal: React.FC<{ setCards: Dispatch<SetStateAction<TCustomCard[]>>, setOpen: Dispatch<boolean> }> = ({ setCards, setOpen }) => {
    const [file, setFile] = useState<File | null>(null);
    const [previewUrl, setPreviewUrl] = useState<string | ArrayBuffer | null>(null);
    const iconBase = baseUrl + "public/regionicons/";

    const imageRef = useRef<HTMLInputElement>(null);
    const nameRef = useRef<HTMLInputElement>(null);
    const descRef = useRef<HTMLTextAreaElement>(null);
    const [cardType, setType] = useState<any[]>([]);
    const [regions, setRegions] = useState<any[]>([]);
    const auth = useRecoilValue(authAtom);

    // const { createACard } = useCustomCardActions();

    const clearFields = () => {
        setFile(null);
        setPreviewUrl(null);
        nameRef.current!.value = "";
        descRef.current!.value = "";
        setType([]);
        setRegions([]);
    }

    const handleSubmit = () => {
        const notFilledFields: string[] = []
        if (!file) notFilledFields.push("card image");
        if (!imageRef.current?.value) notFilledFields.push("card name");
        if (cardType.length === 0) notFilledFields.push("card type");
        if (regions.length === 0) notFilledFields.push("card regions");

        if (notFilledFields.length > 0) {
            alert("You haven't filled out all the necessary fields, you're missing " + notFilledFields.join(", "));
            return;
        }

        const formData = new FormData();
        formData.append("cardname", nameRef.current!.value);
        formData.append("carddescription", descRef.current?.value ? descRef.current.value : "");
        formData.append("regions", regions.reduce((a, b) => a + b));
        formData.append("cardtype", cardType[0]);
        formData.append("imagefile", file!);
        

        fetch("http://localhost:5016/customcard/create-a-card", { method: "POST", body: formData, headers: {Authorization: `Bearer ${auth}`} })
        .then(x => x.json())
        .then(x => { 
            setCards(curr => [x, ...curr]); 
            setOpen(false);
            clearFields();
        }).catch(x => alert(x));
    }

    useEffect(() => {
        if (!file) return;

        const reader = new FileReader();

        reader.onloadend = () => {
            setPreviewUrl(reader.result);
        }

        reader.readAsDataURL(file);
    }, [file]);

    return (
        <div className="create-card-page">
            <div className="card-image-preview">
                <div className="card-image-holder">
                    {previewUrl && <img src={previewUrl as string} alt="Preview" width="100%" height="100%" />}
                </div>
                <div className="card-image-input">
                    <label className="file-button" htmlFor="upload">Upload an image</label>
                    <input ref={imageRef} type="file" accept=".png, .jpg" id="upload" name="imagefile" required onChange={e => setFile(e.target.files ? e.target.files[0] : null)} />
                </div>
            </div>
            <div className="card-create-info">
                <div className="input-fields">
                    <div className="name-section">
                        <label htmlFor="card-name">Card name:</label>
                        <input id="card-name" ref={nameRef} maxLength={50} />
                    </div>
                    <div className="desc-section">
                        <label htmlFor="card-desc">Card description:</label>
                        <textarea id="card-desc" ref={descRef} maxLength={500} />
                    </div>
                    <div className="type-section">
                        <Dropdown setter={setType} style={{width: "100%"}}>
                            <DropdownButton>Card type</DropdownButton>
                            <DropdownContent>
                                <DropdownList>
                                    <DropdownItem name="Follower" value={1}>Follower</DropdownItem>
                                    <DropdownItem name="Champion" value={2}>Champion</DropdownItem>
                                    <DropdownItem name="Spell" value={4}>Spell</DropdownItem>
                                    <DropdownItem name="Landmark" value={8}>Landmark</DropdownItem>
                                    <DropdownItem name="Equipment" value={16}>Equipment</DropdownItem>
                                </DropdownList>
                            </DropdownContent>
                        </Dropdown>
                    </div>
                    <div className="region-section">
                        <Dropdown setter={setRegions} multichoice>
                            <DropdownButton>Card regions</DropdownButton>
                            <DropdownContent>
                                <DropdownList>
                                    <DropdownItem name="Bandle City" value={1}><img alt="" src={iconBase+"bandle_city.png"} />Bandle City</DropdownItem>
                                    <DropdownItem name="Bilgewater" value={2}><img alt="" src={iconBase+"bilgewater.png"} />Bilgewater</DropdownItem>
                                    <DropdownItem name="Demacia" value={4}><img alt="" src={iconBase + "demacia.png"} />Demacia</DropdownItem>
                                    <DropdownItem name="Freljord" value={8}><img alt="" src={iconBase + "freljord.png"} />Feljord</DropdownItem>
                                    <DropdownItem name="Ionia" value={16}><img alt="" src={iconBase + "ionia.png"} />Ionia</DropdownItem>
                                    <DropdownItem name="Noxus" value={32}><img alt="" src={iconBase + "noxus.png"} />Noxus</DropdownItem>
                                    <DropdownItem name="Piltover & Zaun" value={64}><img alt="" src={iconBase + "pnz.png"} />Piltover & Zaun</DropdownItem>
                                    <DropdownItem name="Shadow Isles" value={128}><img alt="" src={iconBase+"shadow_isles.png"} />Shadow Isles</DropdownItem>
                                    <DropdownItem name="Shurima" value={256}><img alt="" src={iconBase+"shurima.png"} />Shurima</DropdownItem>
                                    <DropdownItem name="Targon" value={512}><img alt="" src={iconBase+"targon.png"} />Targon</DropdownItem>
                                    <DropdownItem name="Runterra" value={1024}><img alt="" src={iconBase+"runeterra.png"} />Runeterra</DropdownItem>
                                </DropdownList>
                            </DropdownContent>
                        </Dropdown>
                    </div>
                </div>
                <button onClick={handleSubmit} className="card-submit">Post</button>
            </div>
        </div>
    );
}

export { CardCreateModal }