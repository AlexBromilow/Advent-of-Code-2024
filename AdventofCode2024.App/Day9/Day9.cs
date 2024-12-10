using Advent_of_Code_2024.Day9.Model;
using AdventofCode.Core.Puzzle;
using Microsoft.Extensions.Logging;
using System;
using System.Numerics;


namespace Advent_of_Code_2024.Day9
{
    public class Day9 : BasePuzzle
    {
        public List<Block> Blocks { get; set; }

        public Day9(PuzzleTools tools) : base(tools)
        {
            Blocks = new List<Block>();
        }

        public async void Challenge1()
        {

            var inputData = await GetData().ConfigureAwait(false);

            if (inputData == null)
            {
                Console.WriteLine("No input data found");
                return;
            }

            GenerateBlocks(inputData.DiskMap);

            BigInteger total = 0;

            MoveBlocks();

            foreach (var block in Blocks)
            {
                if (!block.isFile)
                {
                    continue;
                }

                if (!block.fileId.HasValue)
                {
                    continue;
                }

                for (BigInteger i = block.startingIndex; i <= block.endIndex; i++)
                {
                    total += i * block.fileId.Value;
                }
            }

            Console.WriteLine($"Solution is {total}");
            return;
        }

        public async void Challenge2()
        {
            var inputData = await GetData().ConfigureAwait(false);

            if (inputData == null)
            {
                Console.WriteLine("No input data found");
                return;
            }

            GenerateBlocks(inputData.DiskMap);

            BigInteger total = 0;

            MoveBlocksDefrag();

            foreach (var block in Blocks)
            {
                if (!block.isFile)
                {
                    continue;
                }

                if (!block.fileId.HasValue)
                {
                    continue;
                }

                for (BigInteger i = block.startingIndex; i <= block.endIndex; i++)
                {
                    total += i * block.fileId.Value;
                }
            }

            Console.WriteLine($"Solution is {total}");
            return;
        }

        private async Task<Day9Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day9Model>(9, false).ConfigureAwait(false);
        }

        private void GenerateBlocks(string input)
        {
            var isFile = false;
            BigInteger fileId = 0;
            BigInteger blockIndex = 0;
            foreach (var character in input)
            {
                isFile = !isFile;
                var value = BigInteger.Parse(character.ToString());

                if (value == 0)
                {
                    continue;
                }

                var block = new Block
                {
                    startingIndex = blockIndex,
                    endIndex = blockIndex + value - 1,
                    isFile = isFile,
                };

                blockIndex = blockIndex + value;

                if (isFile)
                {
                    block.fileId = fileId;
                    fileId++;
                }

                Blocks.Add(block);
            }
        }

        private void MoveBlocks()
        {
            var index = 0;
            while (true)
            {
                var block = Blocks[index];

                if (block.isFile)
                {
                    index++;
                    continue;
                }

                var newBlock = new Block();

                var endFileBlock = Blocks.Last(x => x.isFile);

                // if the final file block is before where we are, we're done
                if (Blocks.LastIndexOf(endFileBlock) < index)
                {
                    break;
                }

                var spaceLength = (block.endIndex - block.startingIndex) + 1;

                var endFileLength = (endFileBlock.endIndex - endFileBlock.startingIndex) + 1;

                SwapBlocks(block, endFileBlock);

                //if (spaceLength < endFileLength)
                //{

                //    Blocks.RemoveAt(index);

                //    Blocks.Insert(index, new Block
                //    {
                //        startingIndex = block.startingIndex,
                //        endIndex = block.endIndex,
                //        isFile = true,
                //        fileId = endFileBlock.fileId,
                //    }
                //    );

                //    var endFileBlockIndex = Blocks.LastIndexOf(endFileBlock);

                //    Blocks.RemoveAt(endFileBlockIndex);

                //    Blocks.Insert(endFileBlockIndex, new Block
                //    {
                //        startingIndex = endFileBlock.startingIndex,
                //        endIndex = endFileBlock.endIndex - spaceLength,
                //        isFile = true,
                //        fileId = endFileBlock.fileId,
                //    });

                //    Blocks.Insert(endFileBlockIndex + 1, new Block
                //    {
                //        startingIndex = endFileBlock.endIndex - spaceLength + 1,
                //        endIndex = endFileBlock.endIndex,
                //        isFile = false,
                //    }
                //    );
                //}
                //else
                //{
                //    Blocks.RemoveAt(index);

                //    Blocks.Insert(index, new Block
                //    {
                //        startingIndex = block.startingIndex,
                //        endIndex = block.startingIndex + endFileLength - 1,
                //        isFile = true,
                //        fileId = endFileBlock.fileId
                //    });

                //    if (spaceLength != endFileLength)
                //    {
                //        Blocks.Insert(index + 1, new Block
                //        {
                //            startingIndex = block.startingIndex + endFileLength,
                //            endIndex = block.startingIndex + spaceLength - 1,
                //            isFile = false,
                //        });
                //    }

                //    var endFileBlockIndex = Blocks.LastIndexOf(endFileBlock);

                //    Blocks.RemoveAt(endFileBlockIndex);
                //    Blocks.Insert(endFileBlockIndex, new Block
                //    {
                //        startingIndex = endFileBlock.startingIndex,
                //        endIndex = endFileBlock.endIndex,
                //        isFile = false,
                //    });
                //}

                index++;
            }
        }

        private void MoveBlocksDefrag()
        {
            while (true)
            {
                var fileBlock = Blocks.LastOrDefault(x => x.isFile && x.canMove);

                if(fileBlock == null)
                {
                    //No more file blocks that can be moved
                    break;
                }

                var fileBlockIndex = Blocks.IndexOf(fileBlock);

                // get the first gap big enough for this block
                var fileBlockSize = fileBlock.endIndex - fileBlock.startingIndex + 1;
                var firstGapBlock = Blocks.FirstOrDefault(x => fileBlockSize <= x.endIndex - x.startingIndex + 1 && !x.isFile);

                if (firstGapBlock == null || Blocks.IndexOf(firstGapBlock) > fileBlockIndex)
                {
                    //if no earlier gap big enough, fileBlock can't move
                    Blocks.RemoveAt(fileBlockIndex);

                    Blocks.Insert(fileBlockIndex, new Block()
                    {
                        isFile = true,
                        startingIndex = fileBlock.startingIndex,
                        endIndex = fileBlock.endIndex,
                        fileId = fileBlock.fileId,
                        canMove = false
                    });
                    continue;
                }

                SwapBlocks(firstGapBlock, fileBlock);
            }
        }

        private void SwapBlocks(Block gapBlock, Block fileBlock)
        {
            var gapBlockSize = gapBlock.endIndex - gapBlock.startingIndex + 1;

            var fileBlockSize = fileBlock.endIndex - fileBlock.startingIndex + 1;

            var gapBlockIndex = Blocks.IndexOf(gapBlock);
            var fileBlockIndex = Blocks.IndexOf(fileBlock);

            if (gapBlockSize == fileBlockSize)
            {

                Blocks.RemoveAt(gapBlockIndex);

                Blocks.Insert(gapBlockIndex, new Block
                {
                    startingIndex = gapBlock.startingIndex,
                    endIndex = gapBlock.endIndex,
                    fileId = fileBlock.fileId,
                    isFile = true,
                });

                Blocks.RemoveAt(fileBlockIndex);

                Blocks.Insert(fileBlockIndex, new Block
                {
                    startingIndex = fileBlock.startingIndex,
                    endIndex = fileBlock.endIndex,
                    isFile = false,
                });
            }
            else if (gapBlockSize > fileBlockSize)
            {

                Blocks.RemoveAt(fileBlockIndex);

                Blocks.Insert(fileBlockIndex, new Block
                {
                    isFile = false,
                    startingIndex = fileBlock.startingIndex,
                    endIndex = fileBlock.endIndex,
                });

                Blocks.RemoveAt(gapBlockIndex);

                Blocks.Insert(gapBlockIndex, new Block
                {
                    isFile = true,
                    fileId = fileBlock.fileId,
                    startingIndex = gapBlock.startingIndex,
                    endIndex = gapBlock.startingIndex + fileBlockSize - 1
                });

                Blocks.Insert(gapBlockIndex + 1, new Block
                {
                    isFile = false,
                    startingIndex = gapBlock.startingIndex + fileBlockSize,
                    endIndex = gapBlock.endIndex
                });
            }
            else
            {
                // fileBlockSize > gapBlockSize

                Blocks.RemoveAt(gapBlockIndex);

                Blocks.Insert(gapBlockIndex, new Block
                {
                    isFile = true,
                    fileId = fileBlock.fileId,
                    startingIndex = gapBlock.startingIndex,
                    endIndex = gapBlock.endIndex,
                });

                Blocks.RemoveAt(fileBlockIndex);

                Blocks.Insert(fileBlockIndex, new Block
                {
                    isFile = true,
                    fileId = fileBlock.fileId,
                    startingIndex = fileBlock.startingIndex,
                    endIndex = fileBlock.startingIndex + (fileBlockSize - gapBlockSize) - 1
                });

                Blocks.Insert(fileBlockIndex + 1, new Block
                {
                    isFile = false,
                    startingIndex = fileBlock.startingIndex + (fileBlockSize - gapBlockSize),
                    endIndex = fileBlock.endIndex
                });
            }
        }
    }

    public class Block
    {
        public Block() { }

        public bool isFile { get; set; }

        public BigInteger? fileId { get; set; }

        public BigInteger startingIndex { get; set; }

        public BigInteger endIndex { get; set; }

        public bool canMove { get; set; } = true;
    }
}

