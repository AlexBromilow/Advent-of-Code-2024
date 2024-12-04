using AdventofCode.Core.Shared.Enum;
using AdventofCode.Core.Shared.Models;

namespace AdventofCode.Core.Shared.Services
{
    public class CoordinateService
    {
        public Coordinate? GetNextCoordinate(Coordinate currentCoord, DirectionEnum direction, int MaxX, int MaxY)
        {
            Coordinate? nextCoordinate = new Coordinate();

            switch (direction)
            {
                case DirectionEnum.Left:
                    if (currentCoord.XCoordinate == 0)
                    {
                        return null;
                    }
                    nextCoordinate.XCoordinate = currentCoord.XCoordinate - 1;
                    nextCoordinate.YCoordinate = currentCoord.YCoordinate;
                    break;
                case DirectionEnum.Right:
                    if (currentCoord.XCoordinate == MaxX)
                    {
                        return null; 
                    }
                    nextCoordinate.XCoordinate = currentCoord.XCoordinate + 1;
                    nextCoordinate.YCoordinate = currentCoord.YCoordinate;
                    break;
                case DirectionEnum.Up:
                    if (currentCoord.YCoordinate == 0)
                    {
                        return null; 
                    }
                    nextCoordinate.YCoordinate = currentCoord.YCoordinate - 1;
                    nextCoordinate.XCoordinate = currentCoord.XCoordinate;
                    break;
                case DirectionEnum.Down:
                    if (currentCoord.YCoordinate == MaxY)
                    {
                        return null; 
                    }
                    nextCoordinate.YCoordinate = currentCoord.YCoordinate + 1;
                    nextCoordinate.XCoordinate = currentCoord.XCoordinate;
                    break;
                case DirectionEnum.NE:
                    if (currentCoord.YCoordinate == 0 || currentCoord.XCoordinate == MaxX)
                    {
                        return null;
                    }
                    nextCoordinate.YCoordinate = currentCoord.YCoordinate - 1;
                    nextCoordinate.XCoordinate = currentCoord.XCoordinate + 1;
                    break;
                case DirectionEnum.NW:
                    if (currentCoord.YCoordinate == 0 || currentCoord.XCoordinate == 0)
                    {
                        return null;
                    }
                    nextCoordinate.YCoordinate = currentCoord.YCoordinate - 1;
                    nextCoordinate.XCoordinate = currentCoord.XCoordinate - 1;
                    break;
                case DirectionEnum.SE:
                    if (currentCoord.YCoordinate == MaxY || currentCoord.XCoordinate == MaxX)
                    {
                        return null;
                    }
                    nextCoordinate.YCoordinate = currentCoord.YCoordinate + 1;
                    nextCoordinate.XCoordinate = currentCoord.XCoordinate + 1;
                    break;
                case DirectionEnum.SW:
                    if (currentCoord.YCoordinate == MaxY || currentCoord.XCoordinate == 0)
                    {
                        return null;
                    }
                    nextCoordinate.YCoordinate = currentCoord.YCoordinate + 1;
                    nextCoordinate.XCoordinate = currentCoord.XCoordinate - 1;
                    break;

            }

            return nextCoordinate;
        }
    }
}

