function tests(dd) {
    alert(dd);
}

(function () {
    'use strict';

    /**
     * Initialize a Timesheet
     */
    var Timesheet = function (container, min, max, data) {
        this.container = container;
        this.selectedIndex = undefined;
        this.selectedData = [];
        this.data = [];
        this.year = {
            min: min,
            max: max
        };
        this.parse(data || []);
        if (typeof document !== 'undefined') {
            this.container = (typeof container === 'string') ? document.querySelector('#' + container) : container;
            this.drawSections(); //�e��l
            this.insertData();//��J�ƾڸ�u��
        }
        $('#' + container).on('click', 'div', function () {
            this.selectedIndex = $(this).index();
            this.selectedData = data[$(this).index()];
        });
    };
    /**
     * Insert data into Timesheet
     */
    Timesheet.prototype.insertData = function () {
        var html = [];
        var widthMonth = this.container.querySelector('.scale section').offsetWidth;
        for (var n = 0, m = this.data.length; n < m; n++) {
            var cur = this.data[n];
            var bubble = this.createBubble(widthMonth, this.year.min, this.year.max, cur.start, cur.end);
            //bubble.getStartOffset()�e���Ŧh��  bubble.getWidth()�϶�����
            var sLabel = cur.label;
            var iStartOffset = bubble.getStartOffset();
            var iWidth = bubble.getWidth();
            var line = [
                '<span style="cursor:pointer; margin-left: ' + iStartOffset + 'px; width: ' + iWidth + 'px;" class="bubble bubble-' + (cur.type || 'default') + '" data-duration="' + (cur.end ? Math.round((cur.end - cur.start) / 1000 / 60 / 60 / 24 / 39) : '') + '"></span>'
            ].join('');

            var lineData = [
                //'<span style="margin-left: ' + bubble.getStartOffset() + 'px; width: ' + bubble.getWidth() + 'px;" '+ ' data-duration="' + (cur.end ? Math.round((cur.end - cur.start) / 1000 / 60 / 60 / 24 / 39) : '') + '"></span>',
                //'<span class="date">' + bubble.getDateLabel() + '</span> ',
                //'<span class="label">' + cur.label + '</span>',
                '<span class="label" onClick="alert(\'' + sLabel + '\')" title="' + sLabel + '"  style="cursor:pointer;margin-left: ' + iStartOffset + 'px;max-width : ' + iWidth + 'px;">' + sLabel + '</span>'
            ].join('');
            html.push('<div>' + '<li>' + lineData + '</li>' + '<li>' + line + '</li>' + '</div>');
            //html.push();

        }
        this.container.innerHTML += '<ul class="data">' + html.join('') + '</ul>';
    };
    Timesheet.prototype.BubbleClick = function () {
        var element = document.querySelector('#' + this.container);
        element.addEventListener("click", function (e) {
            alert('something');
        }, false);
    };
    Timesheet.prototype.getSelectedData = function () {
        return this.data;
    };
    /**
     * Draw section labels
     */

    //�e��l
    Timesheet.prototype.drawSections = function () {
        var html = [];
        var Mounth = this.countMonth(this.year.min, this.year.max) + 1;
        var minDates = moment(this.year.min, "YYYY/MM/DD").format("YYYY/MM/DD").split('/');
        var minYear = minDates[0];
        var minMounth = minDates[1];
        var maxDates = moment(this.year.max, "YYYY/MM/DD").format("YYYY/MM/DD").split('/');
        var maxYear = maxDates[0];
        var maxMounth = maxDates[1];

        var sectionYear = minYear * 1;
        var sectionMounth = minMounth * 1;
        for (var c = 0; c <= Mounth; c++) {
            if (sectionMounth != 12) {
                html.push("<section style='width:" + "60px'>" + sectionYear + '/' + sectionMounth + '</section>');
            } else if (sectionMounth == 12) {
                html.push("<section style='width:" + "60px'>" + sectionYear + '/' + sectionMounth + '</section>');
                sectionMounth = 0;
                sectionYear += 1;
            }
            sectionMounth = sectionMounth + 1;
        }

        this.container.className = 'timesheet color-scheme-default';
        this.container.setAttribute("style", "width:" + (60 * Mounth) + "px");
        this.container.innerHTML = '<div class="scale">' + html.join('') + '</div>';
    };

    /**
     * Parse data string
     */
    Timesheet.prototype.parseDate = function (date) {
        if (date.indexOf('/') === -1) {
            date = new Date(parseInt(date, 10), 0, 1);
            date.hasMonth = false;
        } else {
            date = date.split('/');
            date = new Date(parseInt(date[1], 10), parseInt(date[0], 10) - 1, 1);
            date.hasMonth = true;
        }

        return date;
    };
    Timesheet.prototype.countMonth = function (min, max) {
        min = min.split('/');
        min = parseInt(min[0]) * 12 + parseInt(min[1]);
        max = max.split('/');
        max = parseInt(max[0]) * 12 + parseInt(max[1]);
        var m = Math.abs(max - min);
        return m;
    };
    /**
     * Parse passed data
     */
    //��ƳB�z
    Timesheet.prototype.parse = function (data) {
        var minYear = moment(this.year.min, "YYYY/MM/DD").toDate();
        var maxYear = moment(this.year.max, "YYYY/MM/DD").toDate();
        for (var n = 0, m = data.length; n < m; n++) {
            //alert(data[n].start);
            var beg = data[n].start != undefined ? moment(data[n].start, "YYYY/MM/DD").toDate() : minYear;
            var end = data[n].end != undefined ? moment(data[n].end, "YYYY/MM/DD").toDate() : maxYear;
            var lbl = data[n].content != undefined ? data[n].content : "";
            var cat = data[n].type != undefined ? data[n].type : 'default';
            //if (beg.getFullYear() < this.year.min) {
            if (beg.getFullYear() < minYear) {
                //this.year.min = beg;�ݭ�
            }

            var maxYear = moment(this.year.max, "YYYY/MM/DD").toDate();
            if (end && end.getFullYear() > maxYear) {
                //this.year.max = end;
            } else if (beg.getFullYear() > maxYear) {
                //this.year.max = beg;�ݭ�
            }
            //if (end && end.getFullYear() > this.year.max) {
            //    this.year.max = end.getFullYear();
            //} else if (beg.getFullYear() > this.year.max) {
            //    this.year.max = beg.getFullYear();
            //}
            this.data.push({start: beg, end: end, label: lbl, type: cat});
        }
    };

    /**
     * Wrapper for adding bubbles
     */
    Timesheet.prototype.createBubble = function (wMonth, min, max, start, end) {

        return new Bubble(wMonth, min, max, start, end);
    };
    /**
     * Timesheet Bubble
     */
    var Bubble = function (wMonth, min, max, start, end) {

        this.min = min;
        this.max = max;
        this.start = moment(start, "YYYY/MM/DD").format("YYYY/MM/DD");
        this.end = moment(end, "YYYY/MM/DD").format("YYYY/MM/DD");
        this.total = this.countMonth(this.min, this.max);
        this.dur = this.countMonth(this.start, this.end);
        this.widthMonth = wMonth;
    };

    /**
     * Format month number
     */
    Bubble.prototype.formatMonth = function (num) {
        num = parseInt(num, 10);
        return num >= 10 ? num : '0' + num;
    };

    /**
     * Calculate starting offset for bubble
     */
    Bubble.prototype.getStartOffset = function () {
        var StartOffset = this.countMonth(this.min, this.start);
        return this.widthMonth * StartOffset;
        //return (this.widthMonth / 12) * (12 * (this.start.getFullYear() - this.min) + this.start.getMonth());
    };
    Bubble.prototype.countMonth = function (min, max) {
        min = min.split('/');
        min = parseInt(min[0]) * 12 + parseInt(min[1]);
        max = max.split('/');
        max = parseInt(max[0]) * 12 + parseInt(max[1]);
        var m = Math.abs(max - min);
        return m;
    };
    /**
     * Get count of full years from start to end
     */
    Bubble.prototype.getFullYears = function () {
        return ((this.end && this.end.getFullYear()) || this.start.getFullYear()) - this.start.getFullYear();
    };

    /**
     * Get count of all months in Timesheet Bubble
     */
    Bubble.prototype.getMonths = function () {
        var fullYears = this.getFullYears();
        var months = 0;

        if (!this.end) {
            months += !this.start.hasMonth ? 12 : 1;
        } else {
            if (!this.end.hasMonth) {
                months += 12 - (this.start.hasMonth ? this.start.getMonth() : 0);
                months += 12 * (fullYears - 1 > 0 ? fullYears - 1 : 0);
            } else {
                months += this.end.getMonth() + 1;
                months += 12 - (this.start.hasMonth ? this.start.getMonth() : 0);
                months += 12 * (fullYears - 1);
            }
        }

        return months;
    };

    /**
     * Get bubble's width in pixel
     */
    Bubble.prototype.getWidth = function () {
        var EndOffset = this.countMonth(this.min, this.end);
        //return (this.widthMonth / 12) * this.getMonths();
        return (this.widthMonth) * EndOffset;
    };

    /**
     * Get the bubble's label �u�����W��
     */
    Bubble.prototype.getDateLabel = function () {
        var start = moment(this.start).format("YYYY/MM/DD");
        var end = moment(this.end).format("YYYY/MM/DD");
        return [
            start, end
        ].join('~');
    };

    window.Timesheet = Timesheet;
})();