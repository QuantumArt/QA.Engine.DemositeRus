import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Apollo, gql } from 'apollo-angular';
import { map } from 'rxjs/operators';

const GET_EVENTS = gql`
  query getEvents {
    events {
      items {
        id
        title
        text
        textBelow
        eventDate
        eventImages {
          id
          title
          image
          sortOrder
        }
      }
    }
  }
`;

interface EventsQueryResult {
  events: {
    items: {
      id: number;
      title: string;
      text: string;
      textBelow?: string;
      eventDate: string;
      eventImages: {
        id: number;
        title: string;
        image: string;
        sortOrder: number;
      }[];
    }[];
  }
}

export interface MediaEventImage {
  id: number;
  title: string;
  image: string;
}

export interface MediaEvent {
  id: number;
  title: string;
  text: string;
  textBelow?: string;
  images: MediaEventImage[];
}

@Injectable()
export class MediaPageService {

  constructor(private readonly apollo: Apollo) {
  }

  public getEvents(): Observable<MediaEvent[]> {
    return this.apollo
      .watchQuery<EventsQueryResult>({
        query: GET_EVENTS
      })
      .valueChanges.pipe(
        map(({ data }) => {
          if (!data?.events?.items?.length) {
            return [];
          }

          const events = data.events.items
            .map(({ id, title, text, textBelow, eventDate, eventImages }) => ({
              id,
              title,
              text,
              textBelow,
              eventDate,
              images: eventImages?.map(({ id, title, image, sortOrder }) => ({
                id,
                title,
                image,
                sortOrder
              })) ?? []
            }));

          for (const event of events) {
            event.images?.sort((a, b) => a.sortOrder - b.sortOrder);
          }

          events.sort((a, b) => new Date(b.eventDate).getTime() - new Date(a.eventDate).getTime());

          return events;
        })
      );
  }
}
