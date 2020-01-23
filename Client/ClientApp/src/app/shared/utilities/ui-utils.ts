import { ElementRef } from '@angular/core';

export const toggleAllCollapsible = (event: any, container: ElementRef) => {
    if (event.currentTarget.classList.contains('collapsed')) {
        event.currentTarget.classList.remove('collapsed');
        container.nativeElement.querySelectorAll('.panel-collapse').forEach(element => {
            element.classList.add('show');
            element.classList.remove('hide');
        });
        container.nativeElement.querySelectorAll('.arrow').forEach(element => {
            element.classList.remove('collapsed');
        });
    } else {
        // event.currentTarget.classList.remove('collapsed');
        event.currentTarget.classList.add('collapsed');
        container.nativeElement.querySelectorAll('.arrow').forEach(element => {
            element.classList.add('collapsed');
        });
        container.nativeElement.querySelectorAll('.panel-collapse').forEach(element => {
            element.classList.remove('show');
            element.classList.add('hide');
        });
    }
};


export function loadMedia() {
    document.querySelectorAll('div[data-oembed-url]').forEach((element: any) => {
        const anchor = document.createElement('a');

        anchor.setAttribute('href', element.dataset.oembedUrl);
        anchor.className = 'embedly-card';

        element.appendChild(anchor);
    });
}


export function drawCallback<T>(table: any, data: Array<T> = []) {
    let row = table.nativeElement.querySelector('.dataTables_empty');
    if (row) {
        row = row.parentElement;
        if (!data.length) {
            row.style.display = 'all';
        } else {
            row.style.display = 'none';
        }
    }
}
