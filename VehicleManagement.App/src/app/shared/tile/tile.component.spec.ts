import {
  byTestId,
  createComponentFactory,
  Spectator,
  SpectatorFactory,
} from '@ngneat/spectator';

import { TileComponent } from './tile.component';

describe('TileComponent', () => {
  let spectator: Spectator<TileComponent>;
  const title: string = 'I am a tester';

  const createTileComponent: SpectatorFactory<TileComponent> =
    createComponentFactory({
      component: TileComponent,
      shallow: true,
    });

  beforeEach(async () => {
    spectator = createTileComponent({ props: { title: title } });
  });

  it('creates the tile component', () => {
    expect(spectator.component).toBeTruthy();
  });

  it('renders the title', () => {
    expect(spectator.query(byTestId('title'))).toHaveText(title);
  });
});
