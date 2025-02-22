using aoc24;

namespace tests;
public class TestDay20 {
  private const string example1 = @"#############################################################################################################################################
#.......###...###...#.....#...#.....#...#.....#...#...#.....#.........#...###...#...###.........#.......###...#...###.........#.....#.......#
#.#####.###.#.###.#.#.###.#.#.#.###.#.#.#.###.#.#.#.#.#.###.#.#######.#.#.###.#.#.#.###.#######.#.#####.###.#.#.#.###.#######.#.###.#.#####.#
#.#...#.....#.#...#.#.#...#.#.#...#.#.#.#...#.#.#.#.#.#.#...#.......#...#...#.#.#.#...#.....#...#...#...#...#...#...#.......#.#.#...#.#.....#
#.#.#.#######.#.###.#.#.###.#.###.#.#.#.###.#.#.#.#.#.#.#.#########.#######.#.#.#.###.#####.#.#####.#.###.#########.#######.#.#.#.###.#.#####
#...#.......#.#...#...#.#...#.#...#.#.#.#...#.#.#.#.#.#.#.......###.....#...#.#.#...#.#...#.#...###.#...#...#.......#...###.#.#.#.#...#.....#
###########.#.###.#####.#.###.#.###.#.#.#.###.#.#.#.#.#.#######.#######.#.###.#.###.#.#.#.#.###.###.###.###.#.#######.#.###.#.#.#.#.#######.#
#.......#...#.....#.....#...#.#...#.#.#.#.#...#.#.#.#.#.#.......#.......#...#.#...#.#.#.#.#...#.....#...###.#.........#.....#...#...#.......#
#.#####.#.#########.#######.#.###.#.#.#.#.#.###.#.#.#.#.#.#######.#########.#.###.#.#.#.#.###.#######.#####.#########################.#######
#.....#.#...#.....#.#...###.#.#...#.#.#...#.#...#.#.#.#.#...#...#.......#...#.#...#.#.#.#.#...#.......#...#.#.....#.....#...#.....#...###...#
#####.#.###.#.###.#.#.#.###.#.#.###.#.#####.#.###.#.#.#.###.#.#.#######.#.###.#.###.#.#.#.#.###.#######.#.#.#.###.#.###.#.#.#.###.#.#####.#.#
#...#.#...#...#...#.#.#...#.#.#...#.#...#...#...#...#.#.#...#.#.........#...#.#.###.#...#.#...#...#...#.#...#...#.#.###...#...###...###...#.#
#.#.#.###.#####.###.#.###.#.#.###.#.###.#.#####.#####.#.#.###.#############.#.#.###.#####.###.###.#.#.#.#######.#.#.###################.###.#
#.#.#...#.....#...#.#...#.#.#.....#.....#...###.....#.#.#.###.............#.#.#...#...#...#...#...#.#.#.#.......#...#...###...###...#...#...#
#.#.###.#####.###.#.###.#.#.###############.#######.#.#.#.###############.#.#.###.###.#.###.###.###.#.#.#.###########.#.###.#.###.#.#.###.###
#.#...#.#.....###.#.###.#.#.......#.....#...#...###.#...#...###...#.....#.#...#...###.#...#...#.#...#...#.........#...#.#...#...#.#.#.#...###
#.###.#.#.#######.#.###.#.#######.#.###.#.###.#.###.#######.###.#.#.###.#.#####.#####.###.###.#.#.###############.#.###.#.#####.#.#.#.#.#####
#...#...#...###...#.#...#.#...###...#...#.#...#...#.#.......#...#.#...#.#.#.....#...#...#.#...#...#...............#.#...#.....#.#.#.#.#.#...#
###.#######.###.###.#.###.#.#.#######.###.#.#####.#.#.#######.###.###.#.#.#.#####.#.###.#.#.#######.###############.#.#######.#.#.#.#.#.#.#.#
###...#...#...#...#.#...#...#.#...###.#...#.....#...#.#...###...#.#...#.#.#...###.#.#...#.#.......#...#...#.....###.#...#...#.#...#.#.#...#.#
#####.#.#.###.###.#.###.#####.#.#.###.#.#######.#####.#.#.#####.#.#.###.#.###.###.#.#.###.#######.###.#.#.#.###.###.###.#.#.#.#####.#.#####.#
#.....#.#...#.....#.....#.....#.#.#...#.....#...#.....#.#.#...#.#.#...#.#.#...#...#.#...#...#.....#...#.#.#...#...#.#...#.#.#.#.....#.#...#.#
#.#####.###.#############.#####.#.#.#######.#.###.#####.#.#.#.#.#.###.#.#.#.###.###.###.###.#.#####.###.#.###.###.#.#.###.#.#.#.#####.#.#.#.#
#.....#...#...#...#.....#.###...#.#...#.....#...#.#...#.#...#...#.....#...#...#...#...#...#.#.....#...#.#...#...#.#.#.....#.#.#.#...#.#.#...#
#####.###.###.#.#.#.###.#.###.###.###.#.#######.#.#.#.#.#####################.###.###.###.#.#####.###.#.###.###.#.#.#######.#.#.#.#.#.#.#####
#...#.#...###...#...#...#.....#...#...#.#...#...#.#.#.#...#.................#...#...#.#...#...#...#...#.#...#...#...#.....#...#...#.#.#.....#
#.#.#.#.#############.#########.###.###.#.#.#.###.#.#.###.#.###############.###.###.#.#.#####.#.###.###.#.###.#######.###.#########.#.#####.#
#.#.#...#...........#.........#...#...#.#.#.#...#.#.#...#...#.....#...#...#...#...#.#.#...#...#...#...#.#.#...#...#...###...........#...#...#
#.#.#####.#########.#########.###.###.#.#.#.###.#.#.###.#####.###.#.#.#.#.###.###.#.#.###.#.#####.###.#.#.#.###.#.#.###################.#.###
#.#...###.........#...###...#...#...#.#...#.....#.#...#.#...#...#.#.#...#.###...#.#.#.....#.#...#...#...#.#.#...#...#...###...#.........#.###
#.###.###########.###.###.#.###.###.#.###########.###.#.#.#.###.#.#.#####.#####.#.#.#######.#.#.###.#####.#.#.#######.#.###.#.#.#########.###
#.#...#...#...#...#...#...#.....#...#...#...#.....#...#.#.#.#...#.#.#.....#...#.#.#...#...#.#.#...#.#...#...#.......#.#...#.#...#.......#...#
#.#.###.#.#.#.#.###.###.#########.#####.#.#.#.#####.###.#.#.#.###.#.#.#####.#.#.#.###.#.#.#.#.###.#.#.#.###########.#.###.#.#####.#####.###.#
#.#.....#...#.#...#.###.....#...#.#...#...#.#.#...#...#.#.#.#...#.#.#.#...#.#.#.#.###...#.#.#...#.#.#.#.....#...#...#.#...#.#.....#...#...#.#
#.###########.###.#.#######.#.#.#.#.#.#####.#.#.#.###.#.#.#.###.#.#.#.#.#.#.#.#.#.#######.#.###.#.#.#.#####.#.#.#.###.#.###.#.#####.#.###.#.#
#.#.........#.....#.....###...#.#.#.#...#...#.#.#...#.#.#.#...#.#.#.#...#.#.#.#.#...#...#.#.#...#.#.#.#...#...#...#...#.#...#.......#...#...#
#.#.#######.###########.#######.#.#.###.#.###.#.###.#.#.#.###.#.#.#.#####.#.#.#.###.#.#.#.#.#.###.#.#.#.#.#########.###.#.#############.#####
#.#.#.......#.........#...#.....#...#...#...#...###...#.#...#.#.#...#.....#.#.#...#.#.#.#.#.#...#.#.#.#.#.....#...#...#.#.#...........#.....#
#.#.#.#######.#######.###.#.#########.#####.###########.###.#.#.#####.#####.#.###.#.#.#.#.#.###.#.#.#.#.#####.#.#.###.#.#.#.#########.#####.#
#...#.........#...###...#...#...#...#.....#.........#...#...#.#.....#.#...#.#...#.#.#.#.#.#...#.#...#...#.....#.#.#...#...#.#.......#...#...#
###############.#.#####.#####.#.#.#.#####.#########.#.###.###.#####.#.#.#.#.###.#.#.#.#.#.###.#.#########.#####.#.#.#######.#.#####.###.#.###
#...........#...#.#.....#...#.#...#.....#...#...#...#...#...#.#.....#...#.#...#.#.#.#.#.#.###...###...###.....#.#.#.#.....#...#...#...#.#...#
#.#########.#.###.#.#####.#.#.#########.###.#.#.#.#####.###.#.#.#########.###.#.#.#.#.#.#.#########.#.#######.#.#.#.#.###.#####.#.###.#.###.#
#...#.....#.#...#...#...#.#...#.........###...#...###...#...#.#.#.........#...#...#...#.#.....#.....#.......#...#...#...#.#.....#.#...#.#...#
###.#.###.#.###.#####.#.#.#####.#####################.###.###.#.#.#########.###########.#####.#.###########.###########.#.#.#####.#.###.#.###
###...#...#...#.....#.#.#...#...#...#...#...#...#...#...#...#...#.#...#...#.........#...#.....#.......#...#.............#.#.....#.#...#.#.###
#######.#####.#####.#.#.###.#.###.#.#.#.#.#.#.#.#.#.###.###.#####.#.#.#.#.#########.#.###.###########.#.#.###############.#####.#.###.#.#.###
#.......#...#.......#.#.#...#.....#...#...#...#.#.#.###.....#...#...#.#.#.#...#...#.#...#...#########...#.............###.......#.....#.#...#
#.#######.#.#########.#.#.#####################.#.#.#########.#.#####.#.#.#.#.#.#.#.###.###.#########################.#################.###.#
#.#...#...#...#.......#...#...#.................#.#.#.........#...#...#.#.#.#.#.#.#...#...#.###################.......#...#...........#.....#
#.#.#.#.#####.#.###########.#.#.#################.#.#.###########.#.###.#.#.#.#.#.###.###.#.###################.#######.#.#.#########.#######
#.#.#...#...#...#...#...#...#...#...#...###.......#...#.....#...#.#...#.#.#.#.#.#.#...#...#.###################...#.....#...#.........#.....#
#.#.#####.#.#####.#.#.#.#.#######.#.#.#.###.###########.###.#.#.#.###.#.#.#.#.#.#.#.###.###.#####################.#.#########.#########.###.#
#...###...#.....#.#...#...#.......#...#...#.#...........###...#...#...#.#.#.#...#.#.###...#.#####################...#########.......#...#...#
#######.#######.#.#########.#############.#.#.#####################.###.#.#.#####.#.#####.#.#######################################.#.###.###
###...#.....#...#.......#...#...###.....#...#...#...........#...###...#.#.#.#...#...#.....#.###...###E###########################S..#...#...#
###.#.#####.#.#########.#.###.#.###.###.#######.#.#########.#.#.#####.#.#.#.#.#.#####.#####.###.#.###.#################################.###.#
#...#...#...#...###...#...#...#...#...#...#...#...#.......#...#...#...#.#.#...#.....#.......#...#.....#######...###...###.......###...#.#...#
#.#####.#.#####.###.#.#####.#####.###.###.#.#.#####.#####.#######.#.###.#.#########.#########.###############.#.###.#.###.#####.###.#.#.#.###
#.....#.#.....#...#.#...###.#...#.....###.#.#.#...#...#...#.......#...#.#...#...#...###...#...#...#...#####...#.....#...#...#...#...#...#...#
#####.#.#####.###.#.###.###.#.#.#########.#.#.#.#.###.#.###.#########.#.###.#.#.#.#####.#.#.###.#.#.#.#####.###########.###.#.###.#########.#
###...#.......#...#.#...#...#.#.#.......#.#.#.#.#.#...#...#.......###.#...#.#.#.#.###...#...#...#.#.#.#.....#...#.......#...#.....#.......#.#
###.###########.###.#.###.###.#.#.#####.#.#.#.#.#.#.#####.#######.###.###.#.#.#.#.###.#######.###.#.#.#.#####.#.#.#######.#########.#####.#.#
#...#.....#...#.....#...#.....#...#.....#.#.#...#...#...#.#.......#...#...#...#.#.#...#.....#.#...#.#...#.....#.#.....###.#.........#...#...#
#.###.###.#.#.#########.###########.#####.#.#########.#.#.#.#######.###.#######.#.#.###.###.#.#.###.#####.#####.#####.###.#.#########.#.#####
#...#.###...#.........#.#...#.......#...#.#...#.....#.#.#.#.......#...#.#...###...#.....###.#.#.....#.....#...#.....#.....#.#...###...#.....#
###.#.###############.#.#.#.#.#######.#.#.###.#.###.#.#.#.#######.###.#.#.#.###############.#.#######.#####.#.#####.#######.#.#.###.#######.#
#...#.#...............#.#.#.#.......#.#.#.....#...#...#.#.#.......###...#.#...###.........#.#.#.....#.......#...#...#.....#...#.....#.......#
#.###.#.###############.#.#.#######.#.#.#########.#####.#.#.#############.###.###.#######.#.#.#.###.###########.#.###.###.###########.#######
#.....#...........#...#...#...#...#...#...#.......#.....#...###...###...#...#.....#...#...#...#...#...#.....#...#.#...###.#.........#.......#
#################.#.#.#######.#.#.#######.#.#######.###########.#.###.#.###.#######.#.#.#########.###.#.###.#.###.#.#####.#.#######.#######.#
#.........#.......#.#.#.....#...#.........#.......#.............#...#.#.###.....#...#...#####...#...#.#...#.#...#.#...#...#.....###.#.......#
#.#######.#.#######.#.#.###.#####################.#################.#.#.#######.#.###########.#.###.#.###.#.###.#.###.#.#######.###.#.#######
#.....#...#.........#.#...#...#.............###...#...#.....#...#...#.#...#.....#.....#...#...#.....#.....#.#...#.#...#...#.....#...#...#...#
#####.#.#############.###.###.#.###########.###.###.#.#.###.#.#.#.###.###.#.#########.#.#.#.###############.#.###.#.#####.#.#####.#####.#.#.#
###...#...............#...###...#...........#...#...#.#...#.#.#.#.#...#...#.....#...#...#.#.........#.....#.#...#.#.#.....#.....#.....#...#.#
###.###################.#########.###########.###.###.###.#.#.#.#.#.###.#######.#.#.#####.#########.#.###.#.###.#.#.#.#########.#####.#####.#
#...#.................#.#.......#...###...###.#...###...#.#...#.#.#...#.#...#...#.#.......#...#...#...#...#.....#.#.#.....#...#.....#.#...#.#
#.###.###############.#.#.#####.###.###.#.###.#.#######.#.#####.#.###.#.#.#.#.###.#########.#.#.#.#####.#########.#.#####.#.#.#####.#.#.#.#.#
#.#...###...........#...#...#...###.....#.....#...#...#...#...#...#...#...#.#...#...###...#.#...#.....#.....#...#.#.....#.#.#.#.....#.#.#.#.#
#.#.#####.#########.#######.#.###################.#.#.#####.#.#####.#######.###.###.###.#.#.#########.#####.#.#.#.#####.#.#.#.#.#####.#.#.#.#
#.#.#...#...#.....#...#.....#...###.........#...#...#.#...#.#...###.......#.###...#.#...#.#.#...#...#.#.....#.#.#.......#...#...#...#...#.#.#
#.#.#.#.###.#.###.###.#.#######.###.#######.#.#.#####.#.#.#.###.#########.#.#####.#.#.###.#.#.#.#.#.#.#.#####.#.#################.#.#####.#.#
#...#.#.###...###...#.#.......#...#.......#.#.#.......#.#...#...#.........#...#...#...#...#...#...#.#.#.......#...#...###.........#.....#...#
#####.#.###########.#.#######.###.#######.#.#.#########.#####.###.###########.#.#######.###########.#.###########.#.#.###.#############.#####
###...#.#.....#...#.#.###...#...#.#.......#...###...#...#...#...#...........#.#...#.....###.....#...#...#.........#.#.....#.............#...#
###.###.#.###.#.#.#.#.###.#.###.#.#.#############.#.#.###.#.###.###########.#.###.#.#######.###.#.#####.#.#########.#######.#############.#.#
#...#...#...#...#.#.#.#...#.....#...#...#...###...#...#...#...#.#...#.......#.#...#.......#...#.#.....#.#.......###.#.....#.............#.#.#
#.###.#####.#####.#.#.#.#############.#.#.#.###.#######.#####.#.#.#.#.#######.#.#########.###.#.#####.#.#######.###.#.###.#############.#.#.#
#...#.....#.#...#.#.#.#.#...#...#.....#...#...#...#.....#...#...#.#.#.......#.#...#.......#...#...#...#.........#...#...#...............#.#.#
###.#####.#.#.#.#.#.#.#.#.#.#.#.#.###########.###.#.#####.#.#####.#.#######.#.###.#.#######.#####.#.#############.#####.#################.#.#
###...#...#...#.#...#...#.#...#...#...........#...#.....#.#...#...#.#...#...#.....#.###...#.....#.#...#...###.....#...#.###...###.....#...#.#
#####.#.#######.#########.#########.###########.#######.#.###.#.###.#.#.#.#########.###.#.#####.#.###.#.#.###.#####.#.#.###.#.###.###.#.###.#
#.....#.........#...#...#.#.......#.............#...#...#.#...#...#.#.#.#.......#...#...#...#...#...#...#.....#...#.#.#.....#...#...#.#...#.#
#.###############.#.#.#.#.#.#####.###############.#.#.###.#.#####.#.#.#.#######.#.###.#####.#.#####.###########.#.#.#.#########.###.#.###.#.#
#.#...#.........#.#...#...#.....#.#...............#...###.#.#.....#.#.#.#.......#...#.#.....#.....#...#.....#...#...#...........###.#...#.#.#
#.#.#.#.#######.#.#############.#.#.#####################.#.#.#####.#.#.#.#########.#.#.#########.###.#.###.#.#####################.###.#.#.#
#.#.#...#.......#.#.............#...#.....#...###.....#...#.#.....#.#.#.#.....#.....#.#.#...#...#...#.#...#.#...............#.......#...#.#.#
#.#.#####.#######.#.#################.###.#.#.###.###.#.###.#####.#.#.#.#####.#.#####.#.#.#.#.#.###.#.###.#.###############.#.#######.###.#.#
#...#...#.....#...#...................#...#.#...#.#...#...#...###.#.#.#.#.....#...#...#.#.#.#.#.###.#.....#.................#.#.....#.....#.#
#####.#.#####.#.#######################.###.###.#.#.#####.###.###.#.#.#.#.#######.#.###.#.#.#.#.###.#########################.#.###.#######.#
#.....#.......#.#.....###...#...........###...#.#.#.....#.#...#...#.#.#.#.....#...#...#.#.#.#.#...#...........................#...#...#...#.#
#.#############.#.###.###.#.#.###############.#.#.#####.#.#.###.###.#.#.#####.#.#####.#.#.#.#.###.###############################.###.#.#.#.#
#...............#...#.....#...#.....#...#...#.#.#...#...#.#.###...#.#.#.#...#.#.#####.#.#.#.#.#...###...#...............#.........###.#.#.#.#
###################.###########.###.#.#.#.#.#.#.###.#.###.#.#####.#.#.#.#.#.#.#.#####.#.#.#.#.#.#####.#.#.#############.#.###########.#.#.#.#
#...#.......#.......#...........#...#.#.#.#.#.#...#.#.#...#...#...#.#.#.#.#...#...#...#.#.#.#.#...#...#.#.............#.#...#...#...#...#.#.#
#.#.#.#####.#.#######.###########.###.#.#.#.#.###.#.#.#.#####.#.###.#.#.#.#######.#.###.#.#.#.###.#.###.#############.#.###.#.#.#.#.#####.#.#
#.#.#...#...#.........#...........#...#...#.#...#.#.#.#.....#...###.#.#.#.......#.#.#...#.#.#.#...#.#...#.....#.....#.#...#...#...#...###.#.#
#.#.###.#.#############.###########.#######.###.#.#.#.#####.#######.#.#.#######.#.#.#.###.#.#.#.###.#.###.###.#.###.#.###.###########.###.#.#
#.#.....#.###...###...#...#.....#...###.....#...#.#.#...#...#.......#.#...#...#.#.#.#...#.#.#.#.....#.#...###...###...###...#...#...#...#...#
#.#######.###.#.###.#.###.#.###.#.#####.#####.###.#.###.#.###.#######.###.#.#.#.#.#.###.#.#.#.#######.#.###################.#.#.#.#.###.#####
#.#.....#.....#.....#.....#.#...#.....#.....#.#...#...#.#.#...#.....#...#.#.#.#.#...###.#.#.#.#.......#...#.........#...#...#.#.#.#...#...###
#.#.###.###################.#.#######.#####.#.#.#####.#.#.#.###.###.###.#.#.#.#.#######.#.#.#.#.#########.#.#######.#.#.#.###.#.#.###.###.###
#.#.###.#...#.....#...#...#.#...#...#.....#.#.#...#...#.#.#...#...#.#...#...#.#.......#...#.#.#.......#...#.#.......#.#.#...#.#.#.###...#...#
#.#.###.#.#.#.###.#.#.#.#.#.###.#.#.#####.#.#.###.#.###.#.###.###.#.#.#######.#######.#####.#.#######.#.###.#.#######.#.###.#.#.#.#####.###.#
#.#.#...#.#.#.###.#.#.#.#.#.#...#.#.#.....#...#...#...#.#.#...#...#.#.#.......#.....#...###.#.#.......#.....#.......#.#...#.#.#.#...###...#.#
#.#.#.###.#.#.###.#.#.#.#.#.#.###.#.#.#########.#####.#.#.#.###.###.#.#.#######.###.###.###.#.#.###################.#.###.#.#.#.###.#####.#.#
#.#.#.....#.#.#...#.#.#.#.#.#...#.#.#.....#.....#.....#...#...#.###.#.#.#.....#...#.#...#...#.#...#...#...#.......#...#...#.#.#...#.....#.#.#
#.#.#######.#.#.###.#.#.#.#.###.#.#.#####.#.#####.###########.#.###.#.#.#.###.###.#.#.###.###.###.#.#.#.#.#.#####.#####.###.#.###.#####.#.#.#
#...#.......#.#.#...#...#...#...#.#.#.....#.....#...###.......#...#.#.#...#...#...#...###...#.#...#.#.#.#...###...#.....###...###.#...#.#.#.#
#####.#######.#.#.###########.###.#.#.#########.###.###.#########.#.#.#####.###.###########.#.#.###.#.#.#######.###.#############.#.#.#.#.#.#
#.....#...#...#.#...#.........#...#.#.........#...#...#...#...#...#...#...#...#.........#...#.#...#.#.#.......#...#.............#.#.#.#.#...#
#.#####.#.#.###.###.#.#########.###.#########.###.###.###.#.#.#.#######.#.###.#########.#.###.###.#.#.#######.###.#############.#.#.#.#.#####
#...#...#.#.#...#...#.........#.#...#.........###...#...#.#.#.#.#.....#.#.###.#.........#...#...#.#.#.#.......###.#.........#...#.#.#.#.....#
###.#.###.#.#.###.###########.#.#.###.#############.###.#.#.#.#.#.###.#.#.###.#.###########.###.#.#.#.#.#########.#.#######.#.###.#.#.#####.#
###.#.#...#.#...#.#...###.....#.#...#.........#.....#...#.#.#.#.#...#...#.....#.....#...###...#.#.#.#.#...#.......#.......#.#...#...#...#...#
###.#.#.###.###.#.#.#.###.#####.###.#########.#.#####.###.#.#.#.###.###############.#.#.#####.#.#.#.#.###.#.#############.#.###.#######.#.###
#...#.#.....###.#.#.#.#...#...#.###.#.......#.#.#...#.#...#.#.#...#...#...#...#...#...#.#...#.#.#.#.#...#.#.....#.....#...#.#...###.....#...#
#.###.#########.#.#.#.#.###.#.#.###.#.#####.#.#.#.#.#.#.###.#.###.###.#.#.#.#.#.#.#####.#.#.#.#.#.#.###.#.#####.#.###.#.###.#.#####.#######.#
#...#...#.......#...#.#...#.#.#.#...#.....#...#...#.#.#...#.#...#...#...#.#.#.#.#.#...#...#.#...#.#.###.#.....#.#...#.#.###...#...#.#.....#.#
###.###.#.###########.###.#.#.#.#.#######.#########.#.###.#.###.###.#####.#.#.#.#.#.#.#####.#####.#.###.#####.#.###.#.#.#######.#.#.#.###.#.#
#...#...#...#...#...#...#...#.#.#...#...#.....#.....#.#...#...#.....#.....#.#...#...#.#...#...###.#...#.#...#.#.#...#...#...#...#.#.#...#...#
#.###.#####.#.#.#.#.###.#####.#.###.#.#.#####.#.#####.#.#####.#######.#####.#########.#.#.###.###.###.#.#.#.#.#.#.#######.#.#.###.#.###.#####
#...#...###...#.#.#.#...#.....#.###.#.#.#.....#.......#...#...#.......#...#.#.........#.#...#...#...#.#.#.#...#.#.........#...#...#.#...#...#
###.###.#######.#.#.#.###.#####.###.#.#.#.###############.#.###.#######.#.#.#.#########.###.###.###.#.#.#.#####.###############.###.#.###.#.#
###...#.#.......#.#.#...#.......#...#.#.#...............#...#...#...#...#.#.#.......#...#...#...###...#.#.....#...#...#.....#...###.#.###.#.#
#####.#.#.#######.#.###.#########.###.#.###############.#####.###.#.#.###.#.#######.#.###.###.#########.#####.###.#.#.#.###.#.#####.#.###.#.#
#.....#.#...#...#.#.#...#.........#...#...#...#...#...#.#.....#...#.#.#...#.#.......#...#.#...#.........#...#.#...#.#.#...#.#...#...#.....#.#
#.#####.###.#.#.#.#.#.###.#########.#####.#.#.#.#.#.#.#.#.#####.###.#.#.###.#.#########.#.#.###.#########.#.#.#.###.#.###.#.###.#.#########.#
#.......###...#...#...###...........#####...#...#...#...#.......###...#.....#...........#...###...........#...#.....#.....#.....#...........#
#############################################################################################################################################
";

  [Theory]
  [InlineData(example1, "1321")]
  public void TestFirstPart(string data, string answer) {
    var solver = new Day20();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveFirst());
  }

  [Theory]
  [InlineData(example1, "971737")]
  public void TestSecondPart(string data, string answer) {
    var solver = new Day20();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveSecond());
  }
}
