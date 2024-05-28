import { baseUrl } from "../utils/GlobalVariables";

export const RegionCount: React.FC<{ regName: string, regCount: number }> = ({ regCount, regName }) => {
    return (
        <div className="region-count">
            <div style={{backgroundImage: `url(${baseUrl}public/regionicons/${regName}.png)`}}></div>
            <p>{regCount}</p>
        </div>
    );
}