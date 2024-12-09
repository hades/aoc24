using System.Collections.Immutable;

namespace aoc24;

[ForDay(9)]
public class Day09 : Solver
{
  private string data;

  public void Presolve(string input) {
    data = input.Trim();
  }

  public string SolveFirst() {
    var arr = new List<int>();
    bool file = true;
    int file_id = 0;
    foreach (var ch in data) {
      if (file) {
        Enumerable.Range(0, ch - '0').ToList().ForEach(_ => arr.Add(file_id));
        file_id++;
      } else {
        Enumerable.Range(0, ch - '0').ToList().ForEach(_ => arr.Add(-1));
      }
      file = !file;
    }
    int from_ptr = arr.Count - 1;
    int to_ptr = 0;
    while (from_ptr > to_ptr) {
      if (arr[to_ptr] != -1) {
        to_ptr++;
        continue;
      }
      if (arr[from_ptr] == -1) {
        from_ptr--;
        continue;
      }
      arr[to_ptr] = arr[from_ptr];
      arr[from_ptr] = -1;
      to_ptr++;
      from_ptr--;
    }
    return Enumerable.Range(0, arr.Count)
      .Select(block_id => arr[block_id] > 0 ? ((long)arr[block_id]) * block_id : 0)
      .Sum().ToString();
  }

  public string SolveSecond() {
    var files = new List<(int Start, int Size, int Id)>();
    bool is_file = true;
    int file_id = 0;
    int block_id = 0;
    foreach (var ch in data) {
      if (is_file) {
        files.Add((block_id, ch - '0', file_id));
        file_id++;
      }
      is_file = !is_file;
      block_id += (ch - '0');
    }
    while (true) {
      bool moved = false;
      for (int from_ptr = files.Count - 1; from_ptr >= 1; from_ptr--) {
        var file = files[from_ptr];
        if (file.Id >= file_id) continue;
        file_id = file.Id;
        for (int to_ptr = 0; to_ptr < from_ptr; to_ptr++) {
          if (files[to_ptr + 1].Start - files[to_ptr].Start - files[to_ptr].Size >= file.Size) {
            files.RemoveAt(from_ptr);
            files.Insert(to_ptr + 1, file with { Start = files[to_ptr].Start + files[to_ptr].Size });
            moved = true;
            break;
          }
        }
        if (moved) break;
      }
      if (!moved) break;
    }
    return files.Select(file => ((long)file.Id) * file.Size * (2 * ((long)file.Start) + file.Size - 1) / 2)
      .Sum().ToString();
  }
}