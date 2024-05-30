import { baseUrl } from "../utils/GlobalVariables";

export const RegionCountDetailed: React.FC<{ regName: string, regCount: number }> = ({ regCount, regName }) => {
    const region = (regName.charAt(0).toUpperCase() + regName.slice(1)).replace("_", " ");
    return (
        <div className="region">
            <div className="region-img" style={{ backgroundImage: `url(${baseUrl}public/regionicons/${regName}.png)` }}></div>
            <div className="region-info">
                <section style={{fontSize: "1.4rem"}}>{region}</section>
                <section>{regCount} Cards</section>
            </div>
        </div>
    );
}