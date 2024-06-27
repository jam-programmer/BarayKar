// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function RandomId() {
    var h = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'];
    var k = ['x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', '-', 'x', 'x', 'x', 'x', '-', '4', 'x', 'x', 'x', '-', 'y', 'x', 'x', 'x', '-', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'];
    var id = '', i = 0, rb = Math.random() * 0xffffffff | 0;
    while (i++ < 36) {
        var c = k[i - 1], r = rb & 0xf, v = c == 'x' ? r : (r & 0x3 | 0x8);
        id += (c == '-' || c == '4') ? c : h[v]; rb = i % 8 == 0 ? Math.random() * 0xffffffff | 0 : rb >> 4
    }
    return id
}